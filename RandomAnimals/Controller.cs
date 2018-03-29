/// ETML
/// Auteur      : Sylvain Laydernier
/// Date        : 15.03.2018
/// Description : Controller class, bind view and model

using System;
using System.Collections.Generic;

namespace RandomAnimals
{
    public class Controller
    {
        private Model appModel = null;
        private View appView = null;
        public Model AppModel
        {
            get
            {
                return appModel;
            }

            set
            {
                appModel = value;
            }
        }
        public View AppView
        {
            get
            {
                return appView;
            }

            set
            {
                appView = value;
            }
        }

        #region Public methods
        /// <summary>
        /// Public method called by view when user wants to insert Data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dateTime"></param>
        public void Insert(string name, DateTime dateTime)
        {
            appModel.Insert(name, dateTime);

            this.appView.Write(appModel.Message);
        }

        /// <summary>
        /// Public method called by view when user wants to update animal name data
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="idToUpdate"></param>
        /// <param name="fieldToUpdate"></param>
        public void Update(string newValue, int idToUpdate)
        {
            appModel.Update(newValue, idToUpdate);

            this.appView.Write(appModel.Message);
        }

        /// <summary>
        /// Public method called by view when user wants to update date time data
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="idToUpdate"></param>
        /// <param name="fieldToUpdate"></param>
        public void Update(DateTime newValue, int idToUpdate)
        {
            appModel.Update(newValue, idToUpdate);

            this.appView.Write(appModel.Message);
        }

        /// <summary>
        /// Public method called by view when user wants to delete data
        /// </summary>
        /// <param name="idToDelete"></param>
        public void Delete(int idToDelete)
        {
            appModel.Delete(idToDelete);

            this.appView.Write(appModel.Message);
        }

        /// <summary>
        /// Public method called by view when user wants to see data
        /// </summary>
        public void Select()
        {
            List<string>[] animals = new List<string>[1];
            animals = appModel.Select();
            this.appView.Write(appModel.Message);
            this.appView.WriteSelect(animals);           
        }

        /// <summary>
        /// Public method used on Model creation
        /// </summary>
        public void SetRandomData()
        {
            appModel.SetRandomData();
        }
        #endregion
    }
}
