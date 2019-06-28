using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    static class ProgressView
    {

        public static ProgressBar CreateSimpleProgressBar(int maxProgress, string initialMessage)
        {           
            return new ProgressBar(
                maxProgress,
                initialMessage,
                new ProgressBarOptions
                {
                    ProgressCharacter = '─',
                    CollapseWhenFinished = true,
                    ProgressBarOnBottom = true,
                }
            );           
        }
    }
}
