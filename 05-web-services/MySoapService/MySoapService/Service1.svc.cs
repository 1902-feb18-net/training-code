using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MySoapService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public static readonly string Version = "1.0.0";

        public string GetServiceVersion()
        {
            throw new NotImplementedException();
            //return Version;
        }

        public int DoubleNumber(int num)
        {
            return num * 2;
        }

        public Question GetQuestion(int id)
        {
            if (id == 1)
            {
                return new Question
                {
                    QuestionId = 1,
                    Category = "basic",
                    Rating = 5,
                    DateModified = DateTime.Now
                };
            }

            throw new FaultException("invalid");
        }
    }
}
