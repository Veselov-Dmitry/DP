using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using DP.Model;
using System.Windows.Input;

namespace DP
{
    class NinjectConfigurationModules : NinjectModule
    {
        private string connectionString = "";
        public NinjectConfigurationModules()
        {
        }
        public NinjectConfigurationModules(string cs)
        {
            connectionString = cs;
        }
        public override void Load()
        {
            Bind<Computers>().ToSelf();
            Bind<Departments>().ToSelf();
            Bind<Developers>().ToSelf();
            Bind<DocumentTypes>().ToSelf();
            Bind<Documents>().ToSelf();
            Bind<Employees>().ToSelf();
            Bind<Offices>().ToSelf();
            Bind<PrgSystems>().ToSelf();
            Bind<PrgLangs>().ToSelf();
            Bind<Professions>().ToSelf();
            Bind<RecordHistories>().ToSelf();
            Bind<SubSystems>().ToSelf();
            Bind<sysdiagrams>().ToSelf();
            Bind<Users>().ToSelf();
            Bind<Workplaces>().ToSelf();
            Bind<AccesRights>().ToSelf();

        }

        /*
        public override void Load()
        {
            Bind<OASUEntity>().ToSelf();
            //Bind<OASUEntity>().ToSelf().WithConstructorArgument("name", connectionString);
            /*Bind<Computers>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Departments>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Developers>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<DocumentTypes>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Documents>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Employees>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Offices>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<PrgSystems>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<PrgLangs>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Professions>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<RecordHistories>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<SubSystems>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<sysdiagrams>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Users>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<Workplaces>().ToSelf().WithConstructorArgument("name", connectionString);
            Bind<AccesRights>().ToSelf().WithConstructorArgument("name", connectionString);
        }/***/


    }
}
