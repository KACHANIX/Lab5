using System;
using System.ServiceModel;

namespace LAB4Service
{
    [ServiceContract]
    public interface ILab4Service
    {
        [OperationContract]
        string Calc(DateTime date1, DateTime date2);

        [OperationContract]
        string Day(DateTime date);

        [OperationContract]
        string Check(int year);

        [OperationContract]
        string SerializeFrom();

        [OperationContract]
        void Test();
    }
    
}
