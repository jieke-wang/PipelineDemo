using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDemo
{
    public delegate Task StepDelegate(IStepContext context);
}
