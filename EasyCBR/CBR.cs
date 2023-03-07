using EasyCBR.Contract;
using EasyCBR.Contract.IStage;
using EasyCBR.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyCBR
{
    public sealed class CBR<TEntity> :
        IRetriveStage<TEntity>, 
        IRetainStage<TEntity>,
        IReuseStage<TEntity>,
        IReviseStage<TEntity>
        where TEntity : class
    {
        private TEntity _entitiy = Activator.CreateInstance<TEntity>();

        private List<TEntity> _entities = new List<TEntity>();

        private List<TEntity> _selectedEntities = new List<TEntity>();

        private Dictionary<string, ISimilarityFunction> _props 
            = new Dictionary<string, ISimilarityFunction>();
        
        private CBR() { }

        public static CBR<TEntity> Create(string path)
        {
            //TODO

            return new CBR<TEntity>();
        }

        public static CBR<TEntity> Create(List<TEntity> entities)
            => new CBR<TEntity>
            {
                _entities = entities
            };

        public IRetainStage<TEntity> Retain()
        {
            return this;
        }

        public IRetriveStage<TEntity> Retrieve(TEntity entity, int count)
        {
            /// ToDo
            _selectedEntities = _entities.Take(count).ToList();

            return this;
        }

        public IReuseStage<TEntity> Reuse(ChooseType chooseType = ChooseType.Top)
        {
            return this;
        }

        public IReviseStage<TEntity> Revise(object correctValue)
        {
            return this;
        }

        public CBR<TEntity> SetSimilarityFunctions(params (string property, ISimilarityFunction similarityFunction) [] pairs)
        {
            foreach (var (property, similarityFunction) in pairs)
            {
                _props.Add(property, similarityFunction);
            }

            return this;
        }

        List<TEntity> IRetriveStage<TEntity>.Run() => _entities;

        TEntity IRetainStage<TEntity>.Run() => _entitiy;

        TEntity IReuseStage<TEntity>.Run() => _entitiy;

        TEntity IReviseStage<TEntity>.Run() => _entitiy;
    }
}
