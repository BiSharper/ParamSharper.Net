
global using size_t =
#if TARGET_64BIT
System.Int64
#else
System.Int32
#endif
;