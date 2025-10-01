using client.services;
using client.services.interfaces;

using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;

using SkiaSharp;
using client.ViewModels.Charts;


namespace client.ViewModels
{
    public class DiagramViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public DiagramViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public TestChartViewModel TestChart { get; set; } = new TestChartViewModel();
    }
}
