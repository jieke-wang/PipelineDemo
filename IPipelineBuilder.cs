using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDemo
{
    public interface IPipelineBuilder
    {
        IPipelineBuilder Use(Func<IStepContext, Func<Task>, Task> step);
        IPipelineBuilder Use(Func<StepDelegate, StepDelegate> step);
        StepDelegate Build();
    }
}
