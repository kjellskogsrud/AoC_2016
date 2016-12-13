using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day10
    {
        // Somewhere to store input
        public string[] input;

        // A Dictionary of programmed bots
        Dictionary<int, BalanceBot> programmedBots = new Dictionary<int, BalanceBot>();

        // A dictionary of output bins(also a bot, but it does not give anything)
        Dictionary<int, BalanceBot> outputBins = new Dictionary<int, BalanceBot>();

        public Day10()
        {
            // read the input file
            input = File.ReadAllLines(@"Inputs\Day10.txt");
        }

        public void Solve()
        {
            ProgramBalanceBots();
            RunBots();
            MultiplyBins();
        }

        private void MultiplyBins()
        {
            Console.WriteLine("{0}", outputBins[0].ChipA.Value * outputBins[1].ChipA.Value *outputBins[2].ChipA.Value);
        }
        private void ProgramBalanceBots()
        {
            // Read each line of the configuration
            for (int i = 0; i < input.Length; i++)
            {
                // Possible lines:
                // value ## goes to bot ##
                // bot ### gives low to bot/output ### and high to bot/output ###

                // Given the possible inputs above we can split the string
                string[] line = input[i].Split(' ');

                int botId;

                // Then we can see if this is a 'bot' or value command.
                switch (line[0])
                {
                    case "bot":
                        // So this is a bot config. Now we need to know if the bot exists
                        // So we first get the ID of the bot.
                        botId = int.Parse(line[1]);

                        // Then if this bot does not exist, we make it.
                        if (!programmedBots.ContainsKey(botId))
                            programmedBots.Add(botId, new BalanceBot(botId,false));

                        // Now the bot exists at we can add some programming to it.
                        // We get the low target first
                        string lowTarget = line[5];
                        int lowTargetID = int.Parse(line[6]);

                        // Is the lowTarget a bot or an output?
                        switch (lowTarget)
                        {
                            case "bot":
                                // Does the bot exist? If not make it.
                                if (!programmedBots.ContainsKey(lowTargetID))
                                    programmedBots.Add(lowTargetID, new BalanceBot(lowTargetID,false));

                                // Assign the bot as the target for our low output.
                                programmedBots[botId].SetLowTarget(programmedBots[lowTargetID]);
                                break;

                            case "output":
                                // Does the bin exist? If not make it.
                                if (!outputBins.ContainsKey(lowTargetID))
                                    outputBins.Add(lowTargetID, new BalanceBot(lowTargetID,true));

                                // Assign the bin as the target
                                programmedBots[botId].SetLowTarget(outputBins[lowTargetID]);
                                break;

                            default:
                                // For all unknow cases.
                                break;
                        }

                        // Now the bot exists at we can add some programming to it.
                        // We get the low target first
                        string highTarget = line[10];
                        int highTargetID = int.Parse(line[11]);

                        // Is the lowTarget a bot or an output?
                        switch (highTarget)
                        {
                            case "bot":
                                // Does the bot exist? If not make it.
                                if (!programmedBots.ContainsKey(highTargetID))
                                    programmedBots.Add(highTargetID, new BalanceBot(highTargetID, false));

                                // Assign the bot as the target for our low output.
                                programmedBots[botId].SetHighTarget(programmedBots[highTargetID]);
                                break;

                            case "output":
                                // Does the bin exist? If not make it.
                                if (!outputBins.ContainsKey(highTargetID))
                                    outputBins.Add(highTargetID, new BalanceBot(highTargetID, true));

                                // Assign the bin as the target
                                programmedBots[botId].SetHighTarget(outputBins[highTargetID]);
                                break;

                            default:
                                // For all unknow cases.
                                break;
                        }
                        break;
                    case "value":
                        // What is the value
                        int value = int.Parse(line[1]);

                        // Make the value chip
                        ValueChip inputChip = new ValueChip(value);

                        // Who is suppos to get the Value?
                        botId = int.Parse(line[5]);

                        // Then if this bot does not exist, we make it.
                        if (!programmedBots.ContainsKey(botId))
                            programmedBots.Add(botId, new BalanceBot(botId, false));

                        // Give the value to the bot
                        programmedBots[botId].SetValue(inputChip);

                        break;
                    default:
                        // For all unknow cases.
                        break;
                }
                //Console.WriteLine("Number of bots: {0}",programmedBots.Count());
                //Console.WriteLine("Number of bins: {0}", outputBins.Count());
                //Console.ReadLine();
            }
        }
        private void RunBots()
        {
            foreach (BalanceBot b in programmedBots.Values)
            {
                b.Compare();
            }
        }

        class BalanceBot
        {
            public int Number { get; protected set; }
            public ValueChip ChipA { get; protected set; }  = null;
            public ValueChip ChipB { get; protected set; }  = null;
            private ValueChip SmallChip = null;
            private ValueChip BigChip = null;
            private BalanceBot HighTarget = null;
            private BalanceBot LowTarget = null;
            public bool IsValid { get; protected set; }
            public bool isBin { get; protected set; }

            public BalanceBot(int number,bool isBin)
            {
                this.Number = number;
                IsValid = false;
                this.isBin = isBin;

            }
            public void SetHighTarget(BalanceBot highTarget)
            {
                HighTarget = highTarget;
                Validate();
            }
            public void SetLowTarget(BalanceBot lowTarget)
            {
                LowTarget = lowTarget;
                Validate();
            }
            public void Validate()
            {
                if(HighTarget != null && LowTarget != null)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
            public void SetValue(ValueChip chip)
            {
                if (ChipA == null)
                    ChipA = chip;
                else if (ChipB == null)
                    ChipB = chip;
                Compare();
            }
            public void Compare()
            {
                if (ChipA != null && ChipB != null && IsValid)
                {
                    this.SmallChip = (ChipA.Value <= ChipB.Value) ? ChipA : ChipB;
                    this.BigChip = (ChipA.Value <= ChipB.Value) ? ChipB : ChipA;
                    LowTarget.SetValue(SmallChip);
                    HighTarget.SetValue(BigChip);
                    if (SmallChip.Value == 17 && BigChip.Value == 61)
                        Console.WriteLine("Bot {0} compared 17 vs 61",this.Number);

                    ChipA = null;
                    ChipB = null;
                    SmallChip = null;
                    BigChip = null;

                }
            }
        }
        class ValueChip
        {
            public int Value { get; protected set; }
            public ValueChip(int value)
            {
                this.Value = value;
            }
        }
    }
}
