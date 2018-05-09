using UnityEngine;

public static class AsyncOperationAwaitable  {
	public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOperation) {
		return new AsyncOperationAwaiter(asyncOperation);
	}
}
