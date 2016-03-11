﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Lucene.Net.Toolbox.Contracts;
using Prism.Commands;

namespace Lucene.Net.Toolbox.ViewModels
{
    internal sealed class MainWindowViewModel
    {
        #region Fields

        IDiscovery _discovery;
        private string _status;

        #endregion

        public MainWindowViewModel(IDiscovery discovery)
        {
            _discovery = discovery;

            Text = "Welcome to the analysis viewer. This tool is used to demonstrate how different analyzers process text into tokens. You can edit this text to try different input such as numbers like 23231.23 or characters (this.mail@mailprovider.com). Once happy, select an Analyzer from the list of analyzers found on the current assemblies path and then hit the Analyze button. The tokens produced are shown below and when you select them the right panel shows their attributes, and the corresponding span in the original text is highlighted.";
            
            TokenChangedCommand = new DelegateCommand(OnTokenChanging);
            AnalyzeCommand = new DelegateCommand<IAnalyzer>(OnAnalyzing);
            Analyzers = new ObservableCollection<IAnalyzer>();
            Tokens = new ObservableCollection<IToken>();

            Initialize();
        }

        ~MainWindowViewModel()
        {
            _discovery.Discovered -= OnDiscovering;
            _discovery.Dispose();
        }

        #region Properties

        public string Status { get { return _status; } }
        public string Text { get; set; }

        public IAnalyzer CurrentAnalyzer { get; set; }
        public IToken CurrentToken { get; set; }

        public ObservableCollection<IAnalyzer> Analyzers { get; set; }
        public ObservableCollection<IToken> Tokens { get; set; }

        #endregion

        #region Commands

        public ICommand TokenChangedCommand { get; private set; }
        public ICommand AnalyzeCommand { get; private set; }

        #endregion

        private void Initialize()
        {
            _discovery.Discovered += OnDiscovering;
            _discovery.Discover();

            _status = string.Format("Discovery is {0}", _discovery.IsRunning ? "Running" : "Stopped");
        }

        private void OnTokenChanging()
        {

        }

        private async void OnAnalyzing(IAnalyzer analyzer)
        {
            var tokens = await analyzer.AnalyzeAsync(Text);

            Tokens.Clear();
            Tokens.AddRange(tokens);
        }
        
        private void OnDiscovering(IAnalyzer analyzer, EventArgs e)
        {
            Analyzers.Add(analyzer);

            CurrentAnalyzer = analyzer;
        }
    }
}