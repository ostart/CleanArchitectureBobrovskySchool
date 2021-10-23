using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CleanArchitectureBobrovskySchool
{
    class RobotCleaner
    {
        private State _state {get;set;}
        
        public RobotCleaner()
        {
            _state = new State();
        }

        public void Work(string nameOfFileWithRobotCommands)
        {
            var allCommandsList = File.ReadAllLines(nameOfFileWithRobotCommands).ToList();
            InterpretCommands(allCommandsList);
        }

        private void InterpretCommands(List<string> commands)
        {
            foreach(var command in commands)
            {
                var arrayOfActionsWithParameter = command.Split(' ');
                var action = arrayOfActionsWithParameter[0];
                var parameter = arrayOfActionsWithParameter.Length > 1 ? arrayOfActionsWithParameter[1] : null;
                switch(action)
                {
                    case "move":
                        var meters = Convert.ToInt32(parameter);
                        Move(meters);
                        break;
                    case "turn":
                        var angle = Convert.ToInt32(parameter);
                        Turn(angle);
                        break;
                    case "set":
                        if (Enum.TryParse(parameter, out Tools tool))
                            Set(tool);
                        else
                            Set();
                        break;
                    case "start":
                        Start();
                        break;
                    case "stop":
                        Stop();
                        break;
                    default:
                        throw new NotImplementedException($"Action {action} not supported");
                }
            }
        }

        private void Move(int meters)
        {
            var currentPosition = _state.Position;
            var angleInRadian = Math.PI * _state.AngleInDegrees / 180.0;
            var newPosition = new Position();
            newPosition.X = currentPosition.X + meters * Math.Cos(angleInRadian);
            newPosition.Y = currentPosition.Y + meters * Math.Sin(angleInRadian);
            _state.Position = newPosition;
            
            TransferToCleaner($"POS {_state.Position.X},{_state.Position.Y}");
        }

        private void Turn(int angleDegrees)
        {
            var currentAngle = _state.AngleInDegrees;
            _state.AngleInDegrees = currentAngle + angleDegrees;

            TransferToCleaner($"ANGLE {_state.AngleInDegrees}");
        }

        private void Set(Tools tool = Tools.water)
        {
            _state.Tool = tool;
            var stateName = Enum.GetName<Tools>(_state.Tool);

            TransferToCleaner($"STATE {stateName}");
        } 
        
        private void Start()
        {
            var stateName = Enum.GetName<Tools>(_state.Tool);

            TransferToCleaner($"START WITH {stateName}");
        }
        
        private void Stop()
        {
           TransferToCleaner("STOP");
        }

        private void TransferToCleaner(string message)
        {
            Console.WriteLine(message);
        }
    }
}