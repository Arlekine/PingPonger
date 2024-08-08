public interface IServiceLocator<T1> where T1 : IService
{
    T2 GetService<T2>() where T2 : T1;
}