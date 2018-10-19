using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace Models
{
#region Extension for GameLogic for serializing to XML.
    internal static class GameXMLUtils
    {
        static readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(GameData));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">The game to be converted</param>
        /// <returns>XElement representing the game.</returns>
        public static XElement ToXElement(this GameData game)
        {
            //Using ensures the proper disposal of IDisposalble
            //Equivalent to try-with-resources in java

            
            using (var stream = new MemoryStream())
            {
                //Write the game into the memoryStream
                _serializer.WriteObject(stream, game);
                //Reset position of the stream to the beginning
                stream.Position = 0;
                //Create XmlReader from the stream 
                using(XmlReader reader = XmlReader.Create(stream))
                {
                    //Load XElement from reader
                    return XElement.Load(reader);
                }
            }
        }
        /// <summary>
        /// Deserializes the provided XElement into a GameLogic.
        /// </summary>
        /// <param name="xElement"></param>
        /// <returns></returns>
        public static GameData FromXElement(this XElement xElement)
        {
            return (GameData)_serializer.ReadObject(xElement.CreateReader());
        }

    }
    #endregion

    public class GamePersistenceManager
    {
        private string _fileName;
        public List<GameData> GameList { get; }
        /// <summary>
        /// Loads or creates the file with the provided name.
        /// </summary>
        /// <param name="fileName"></param>
        public GamePersistenceManager(string fileName)
        {
            _fileName = fileName;
            if (File.Exists(fileName))
            {

                //Load the provided file and get Xelemenents contain within.
                var xElements = XDocument.Load(fileName).Root.Elements();
                //Deserialize the XElelemts to GameLogics
                GameList = xElements.Select(xelement => xelement.FromXElement()).Where(data => data.Phase == GameLogic.GamePhase.InProgress).ToList();
            }
            else
            {
                GameList = new List<GameData>();
            }
            

        }

        public void AddGameIfNotPresent(GameData newGame)
        {
            var isPresent = GameList.Where(data => data.Id.Equals(newGame.Id)).Any();
            if (!isPresent)
            {

                GameList.Add(newGame);
            }



        }

        public void RemoveGame(string gameId)
        {
            var game = GameList.FirstOrDefault(data => data.Id.Equals(gameId));
            if(game != null)
            {
                GameList.Remove(game);
            }
        }

        /// <summary>
        /// Load the game with the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns null if no such game exsists</returns>
        public GameData LoadGame(string id) => GameList.Where(game => game.Id.Equals(id)).FirstOrDefault();


        /// <summary>
        /// Saves all games storred in this GamePersistenceManager.
        /// </summary>
        public void Save()
        {
            var document = new XDocument();

            var rootElement = new XElement("games");

            document.Add(rootElement);

            GameList.Where(data => data.Phase == GameLogic.GamePhase.InProgress).ToList().ForEach(game => rootElement.Add(game.ToXElement()));
            document.Save(_fileName);


        }



    }
}
