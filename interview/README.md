
# c++
- const
- mutable
- volatile - 최적화를 하지 않음. 외부에서 값이 바뀔수 있다는 가정, 매번 메모리에서 직접 사용.
- override
- explicit
- move semantics
- copy elision
- vector, array, list, deque, priority_queue, map, unordered_map

# thread
- deadlock - 
- spin lock - context switching을 하지 않고 busy waiting하는 락.
- critical section - 공유 자원을 접근하는 코드. race condition을 막기 위해 lock으로 보호 한다.
- mutex - 상호 베제 lock. cs 구역을 만들때 보통 사용하는 lock.
- semaphore - mutex와 비슷하나 CS 구역에 들어올수 있는 theread 수 제한을 count 하는 장치.
- event -  대기 중인 쓰레드를 깨우는 singal
- lock-free/wait-free - http://concurrencyfreaks.blogspot.com/2013/05/lock-free-and-wait-free-definition-and.html

# network
- iocp - kernel이 thread pool을 관리해서 context switching을 최대한 줄여줌. buffer copy를 최소한으로 줄일 수 있음.
- tcp/udp


# windows
- WaitForSingleObject

# db
- stored procedure -  한층의 보안, 추상화. 호출 수, 호출 데이터가 적어 짐. DB에서 쿼리 최적화를 할 수 있음.

# restapi
- safe / idempotent - safe=const, idempotent=retriable http://restcookbook.com/HTTP%20Methods/idempotency/
