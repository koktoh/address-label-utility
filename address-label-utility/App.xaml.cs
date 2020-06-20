using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using AddressLabelUtility.Views;
using AddressLabelUtility.ViewModels;

namespace AddressLabelUtility
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            // shift-jis を使えるように
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<LabelMakerControl, LabelMakerViewModel>();
            ViewModelLocationProvider.Register<CsvConverterControl, CsvConverterViewModel>();
        }
    }
}
