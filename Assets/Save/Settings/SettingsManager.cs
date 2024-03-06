using System;
using System.IO;
using UnityEngine;

namespace MiniIT.SAVE
{
    public class SettingsManager : ISave<SettingsJSON>, ILoad<SettingsJSON>
    {
        private SettingsManagerConfig _config;

        public SettingsManager(SettingsManagerConfig config)
            => _config = config;

        public void Save(SettingsJSON info)
        {
            string json = JsonUtility.ToJson(info);

            try
            {
                File.WriteAllText(_config.SavePatch, json);
            }
            catch (Exception e)
            {
                Debug.Log($"Warrning: {e}");
            }
        }

        public SettingsJSON Load()
        {
            if(!File.Exists(_config.SavePatch))
                return new SettingsJSON(_config.DefaultMusicVolue, _config.DefaultSFXVolue);

            try
            {
                string json = File.ReadAllText(_config.SavePatch);

                return JsonUtility.FromJson<SettingsJSON>(json);
            }
            catch (Exception e)
            {
                Debug.Log($"Warrning: {e}");

                return new SettingsJSON();
            }
        }
    }
}