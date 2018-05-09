using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine.Profiling;

public class UnityTaskScheduler : TaskScheduler {
        private CustomSampler sampler;

        private BlockingCollection<Task> tasks = new BlockingCollection<Task>();

        private readonly Thread thread = null;

        public UnityTaskScheduler() {
            sampler = CustomSampler.Create("Task");

            thread = new Thread(new ThreadStart(Execute));
            thread.Start();
        }

        private void Execute() {
            Profiler.BeginThreadProfiling("Unity Task Scheduler", "Execute");

            foreach (var task in tasks.GetConsumingEnumerable()) {
                sampler.Begin();

                TryExecuteTask(task);

                sampler.End();
            }

            Profiler.EndThreadProfiling();
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            return tasks.ToArray();
        }

        protected override void QueueTask(Task task) {
            tasks.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
            return false;
        }
}