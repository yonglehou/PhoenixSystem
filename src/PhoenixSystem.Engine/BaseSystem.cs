﻿using System;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine
{
    public abstract class BaseSystem : ISystem
    {
        protected BaseSystem(int priority)
        {
            ID = Guid.NewGuid();
            Priority = priority;
        }

        public Guid ID { get; private set; }

        public int Priority { get; private set; }

        public bool IsActive { get; private set; }

        public void Start()
        {
            IsActive = true;
        }

        public void Stop()
        {
            IsActive = false;
        }

        protected abstract void Update();

        public event EventHandler<GameManagerChangedEventArgs> AddedToGameManager;

        protected virtual void OnAddedToGameManager(IGameManager gameManager)
        {
            AddedToGameManager?.Invoke(this, new GameManagerChangedEventArgs {GameManager = gameManager});
        }

        public event EventHandler RemovedFromGameManager;

        protected virtual void OnRemovedFromGameManager()
        {
            RemovedFromGameManager?.Invoke(this, null);
        }
    }
}