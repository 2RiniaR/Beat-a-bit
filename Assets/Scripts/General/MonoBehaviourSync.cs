using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RineaR.BeatABit.General
{
    public abstract class MonoBehaviourSync : IDisposable
    {
        private readonly BoolReactiveProperty _allEnabled = new(false);
        private readonly BoolReactiveProperty _selfEnabled = new(false);
        private readonly BoolReactiveProperty _sourceEnabled = new(false);
        private CompositeDisposable _disableDisposable;
        private CompositeDisposable _syncDisposable;
        public MonoBehaviour Source { get; private set; }

        public bool Enabled
        {
            get => _selfEnabled.Value;
            set => _selfEnabled.Value = value;
        }

        public virtual void Dispose()
        {
            if (_syncDisposable is { IsDisposed: false }) _syncDisposable.Dispose();
        }

        public void Sync(MonoBehaviourSync source)
        {
            Sync(source.Source);
        }

        public void DisposeOnDisable(params IDisposable[] disposables)
        {
            if (_disableDisposable == null || _disableDisposable.IsDisposed) throw new InvalidOperationException();
            foreach (var disposable in disposables)
            {
                disposable.AddTo(_disableDisposable);
            }
        }

        public void Sync(MonoBehaviour source)
        {
            if (!source) throw new ArgumentNullException();

            if (Source != null) Release();
            _syncDisposable = new CompositeDisposable();
            Source = source;

            _selfEnabled.Merge(_sourceEnabled).Subscribe(_ =>
            {
                _allEnabled.Value = _selfEnabled.Value && _sourceEnabled.Value;
            }).AddTo(_syncDisposable);

            this.AddTo(source);

            _allEnabled.Where(x => x).Subscribe(_ =>
            {
                _disableDisposable = new CompositeDisposable();
                OnEnable();
            }).AddTo(_syncDisposable);

            _allEnabled.Where(x => !x).Subscribe(_ =>
            {
                OnDisable();
                _disableDisposable?.Dispose();
                _disableDisposable = null;
            }).AddTo(_syncDisposable);

            Source.OnEnableAsObservable().Subscribe(_ => { _sourceEnabled.Value = true; }).AddTo(_syncDisposable);

            Source.OnDisableAsObservable().Subscribe(_ => { _sourceEnabled.Value = false; }).AddTo(_syncDisposable);

            Source.OnDestroyAsObservable().Subscribe(_ => { OnDestroy(); }).AddTo(_syncDisposable);

            Source.UpdateAsObservable().Subscribe(_ =>
            {
                if (_allEnabled.Value) Update();
            }).AddTo(_syncDisposable);

            Source.FixedUpdateAsObservable().Subscribe(_ =>
            {
                if (_allEnabled.Value) FixedUpdate();
            }).AddTo(_syncDisposable);

            _sourceEnabled.Value = Source.isActiveAndEnabled;
            _selfEnabled.Value = true;
        }

        public void Release()
        {
            _selfEnabled.Value = false;

            Source = null;
            _syncDisposable.Dispose();
            _syncDisposable = null;
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }
    }
}