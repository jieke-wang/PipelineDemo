using System;
using System.Threading.Tasks;

namespace PipelineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IPipelineBuilder pipelineBuilder = new PipelineBuilder();
            IStepContext stepContext = new StepContext();

            pipelineBuilder.Use(async (context, next) => 
            {
                Console.WriteLine("第一步开始");
                await next();
                Console.WriteLine("第一步结束");
            });

            pipelineBuilder.Use(async (context, next) =>
            {
                Console.WriteLine("第二步开始");
                await next();
                Console.WriteLine("第二步结束");
            }).Use(async (context, next) =>
            {
                Console.WriteLine("第三步开始");
                await next();
                Console.WriteLine("第三步结束");
            });

            pipelineBuilder.Use((context, next) => 
            {
                Console.WriteLine("第四步终结点");
                return Task.CompletedTask;
            });

            var pipeline = pipelineBuilder.Build();
            pipeline(stepContext).Wait();
        }
    }
}
