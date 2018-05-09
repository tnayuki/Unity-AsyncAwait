using System.Collections;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using UnityEngine;

public class AsyncOperationAwaiter : INotifyCompletion {
    private AsyncOperation asyncOperation;
    private System.Action continuation;

    public AsyncOperationAwaiter(AsyncOperation asyncOperation) {
        this.asyncOperation = asyncOperation;

        CoroutineDispatcher.Get().DispatchCoroutine(WrappedCoroutine());
    }

    public bool IsCompleted { 
        get { return asyncOperation.isDone; }
    }
    public void OnCompleted(System.Action continuation) {
        this.continuation = continuation;
    }
    
    public void GetResult() { 
    }

    public IEnumerator WrappedCoroutine() {
        yield return asyncOperation;
        continuation();
    }
}

