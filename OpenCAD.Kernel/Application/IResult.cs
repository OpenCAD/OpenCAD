using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.Kernel.Application
{
    public interface IResult
    {
        string Message { get; }

        void Execute();
    }

    public abstract class BaseWork:IResult
    {
        public string Message { get; private set; }

        protected BaseWork(string message)
        {
            Message = message;
        }

        public abstract void Execute();
    }

    public class Work : BaseWork
    {
        public Action Action { get; private set; }

        public Work(string message, Action action) : base(message)
        {
            Action = action;
        }
        public override void Execute()
        {
            Action();
        }
    }
    public class WorkMessage : BaseWork
    {
        public WorkMessage(string message)
            : base(message)
        {
     
        }
        public override void Execute()
        {
       
        }
    }

    public static class Worker
    {
        public static void Run(Action<string> messageAction, params IEnumerable<IResult>[] results)
        {
            foreach (var result in results)
            {
                foreach (var enumerable in result)
                {
                    messageAction(enumerable.Message);
                    enumerable.Execute();
                }

            }
        }
    }
}