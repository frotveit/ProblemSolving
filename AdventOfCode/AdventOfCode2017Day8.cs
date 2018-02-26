

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class AdventOfCode2017Day8
    {
        public class Result
        {
            public int Max;
            public int ProcessMax;
        }

        public enum RegisterOperation
        {
            Increment,
            Decrement
        }

        public class RegisterCondition
        {
            public string Register;
            public string Condition;
            public int Value;
        }

        public class Instruction
        {
            public string Register;
            public RegisterOperation Operation;
            public int Value;
            public RegisterCondition Condition;
        }

        public class Registers
        {
            public Dictionary<string, int> Data = new Dictionary<string, int>();
            private int _processMax = Int32.MinValue;

            public Registers()
            {
            }

            public Registers(IEnumerable<Instruction> instructions)
            {
                foreach (var instruction in instructions)
                {
                    ProcessInstruction(instruction);
                }
            }

            public void ProcessInstruction(Instruction instruction)
            {
                CheckRegister(instruction.Register);
                if (CheckCondition(instruction.Condition))
                {
                    ApplyInstruction(instruction);
                }
            }

            public void ApplyInstruction(Instruction instruction)
            {
                int value = Data[instruction.Register];
                switch (instruction.Operation)
                {
                    case RegisterOperation.Increment:
                    {
                        value = value + instruction.Value;
                        break;
                    }
                    case RegisterOperation.Decrement:
                    {
                        value = value - instruction.Value;
                        break;
                    }
                }
                Data[instruction.Register] = value;
                if (value > _processMax)
                    _processMax = value;
            }

            public bool CheckCondition(RegisterCondition condition)
            {
                CheckRegister(condition.Register);
                int registerValue = Data[condition.Register];
                switch (condition.Condition)
                {
                    case "==": return registerValue == condition.Value;
                    case "!=": return registerValue != condition.Value;
                    case ">": return registerValue > condition.Value;
                    case "<": return registerValue < condition.Value;
                    case ">=": return registerValue >= condition.Value;
                    case "<=": return registerValue <= condition.Value;
                }

                return false;
            }

            public void CheckRegister(string register)
            {
                if (Data.ContainsKey(register) == false)
                {
                    Data.Add(register, 0);
                }
            }

            public int GetMax()
            {
                int max = Int32.MinValue;
                foreach (var val in Data.Values)
                {
                    if (val > max)
                        max = val;
                }
                return max;
            }

            public int GetProcessMax()
            {
                return _processMax;
            }
        }

        public static Result Solve(string filename)
        {
            string[] filedata = FileUtil.GetFile(filename);
            var instructions = filedata.Select(ParseLine);

            Registers registers = new Registers(instructions);

            return new Result
            {
                Max = registers.GetMax(),
                ProcessMax =registers.GetProcessMax() 
            };
        }

        public static Instruction ParseLine(string line)
        {
            string[] lineItems = line.Split(new []{" "}, StringSplitOptions.RemoveEmptyEntries);
            

            RegisterCondition condition = new RegisterCondition
            {
                Register = lineItems[4],
                Condition = lineItems[5],
                Value = int.Parse(lineItems[6])
            };

            Instruction instruction = new Instruction
            {
                Register = lineItems[0],
                Operation = lineItems[1] == "inc" ? RegisterOperation.Increment : RegisterOperation.Decrement,
                Value = int.Parse(lineItems[2]),
                Condition = condition
            };

            return instruction;
        }
    }
}
