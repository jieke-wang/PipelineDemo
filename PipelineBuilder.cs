using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipelineDemo
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly IList<Func<StepDelegate, StepDelegate>> _components = null;
        private bool _builded = false;

        public PipelineBuilder()
        {
            this._components = new List<Func<StepDelegate, StepDelegate>>();
        }

        public StepDelegate Build()
        {
            if (_builded) throw new InvalidOperationException("Build can only once");
            _builded = true;

            StepDelegate step = context =>
            {
                Console.WriteLine($"Warning: you didn't provide an endpoint on {DateTime.Now:yyyy/MM/dd HH:mm:ss.fff}"); ;
                return Task.CompletedTask;
            };

            foreach (var component in _components.Reverse())
            {
                step = component(step);
            }

            _components.Clear();

            return step;
        }

        public IPipelineBuilder Use(Func<IStepContext, Func<Task>, Task> step)
        {
            return Use(next =>
            {
                return context =>
                {
                    Task nextTask() => next(context); // Task nextTask() { return next(context); };
                    return step(context, nextTask);
                };
            });
        }

        public IPipelineBuilder Use(Func<StepDelegate, StepDelegate> step)
        {
            this._components.Add(step);
            return this;
        }
    }
}
