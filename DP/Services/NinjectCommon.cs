using DP.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Services
{
    public static class NinjectCommon
    {

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel(string nameStringConnectino)
        {
            var kernel = new StandardKernel();
            try
            {
                //!!!выполните привязку интерфейса IRepository к классу FakeRepository
                /*kernel.Bind<IRepository<Cloth>>().To<FakeRepository>();/**/
                kernel.Bind<OASUEntity>().To<OASUEntity>().
                    WithConstructorArgument(nameStringConnectino);/**/
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
        //public static IKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();
        //    try
        //    {
        //        //!!!выполните привязку интерфейса IRepository к классу FakeRepository
        //        /*kernel.Bind<IRepository<Cloth>>().To<FakeRepository>();/**/
        //        kernel.Bind<OASUEntity>().To<OASUEntity>().
        //            WithConstructorArgument("name", "_OASUEntity");/**/
        //        return kernel;
        //    }
        //    catch
        //    {
        //        kernel.Dispose();
        //        throw;
        //    }
        //}
    }
}
