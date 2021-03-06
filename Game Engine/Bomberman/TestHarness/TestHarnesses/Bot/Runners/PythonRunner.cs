﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Domain.Meta;
using TestHarness.Properties;
using TestHarness.Util;

namespace TestHarness.TestHarnesses.Bot.Runners
{
    public class PythonRunner : BotRunner
    {
        public PythonRunner(BotHarness parentHarness)
            : base(parentHarness)
        {
        }

        protected override ProcessHandler CreateProcessHandler()
        {
            var pythonExecutable = Settings.Default.PathToPython3;
            if (ParentHarness.BotMeta.BotType == BotMeta.BotTypes.Python2)
            {
                pythonExecutable = Settings.Default.PathToPython2;
            }

            var processArgs = String.Format("{0} {1} \"{2}\"", ParentHarness.BotMeta.RunFile,
                ParentHarness.PlayerEntity.Key, ParentHarness.CurrentWorkingDirectory);


            return new ProcessHandler(ParentHarness.BotDir, pythonExecutable, processArgs, ParentHarness.Logger);
        }

        protected override void RunCalibrationTest()
        {
            var pythonExecutable = Settings.Default.PathToPython3;
            if (ParentHarness.BotMeta.BotType == BotMeta.BotTypes.Python2)
            {
                pythonExecutable = Settings.Default.PathToPython2;
            }

            var calibrationBot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Calibrations" + Path.DirectorySeparatorChar + "BotCalibrationPython.py");
            var processArgs = String.Format("{0} {1} \"{2}\"", calibrationBot,
                ParentHarness.PlayerEntity.Key, ParentHarness.CurrentWorkingDirectory);

            using (var handler = new ProcessHandler(AppDomain.CurrentDomain.BaseDirectory, pythonExecutable, processArgs, ParentHarness.Logger))
            {
                handler.RunProcess();
            }
        }
    }
}
