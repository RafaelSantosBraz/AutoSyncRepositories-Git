using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    static class ProgressView
    {

        public static ProgressBar CreateSimpleProgressBar(int maxProgress, string initialMessage, bool colapse = true)
        {           
            return new ProgressBar(
                maxProgress,
                initialMessage,
                new ProgressBarOptions
                {
                    ProgressCharacter = '─',
                    CollapseWhenFinished = colapse,
                    ProgressBarOnBottom = false,
                }
            );           
        }
    }
}
