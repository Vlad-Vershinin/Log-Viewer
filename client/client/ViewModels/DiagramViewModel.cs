<<<<<<< HEAD
﻿using client.services;
using client.services.interfaces;
=======
﻿using LiveChartsCore;
>>>>>>> 0f1a112b3f96582bc28ba8a00c2206cf891c24b1
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
<<<<<<< HEAD
        private readonly INavigationService _navigationService;

        public DiagramViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
=======
        public TestChartViewModel TestChart { get; set; } = new TestChartViewModel();
>>>>>>> 0f1a112b3f96582bc28ba8a00c2206cf891c24b1
    }
}
