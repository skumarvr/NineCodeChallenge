using NineCodingChallenge.Models.Assessment;
using System;
using System.Collections.Generic;

namespace NineCodingChallenge.Business.Assessment.Impl
{
    public class SerialManager: ISerialManager
    {
        /// <summary>
        /// Method to filter Serial Json
        /// </summary>
        /// <param name="serialJson">Serial json</param>
        /// <returns>Filtered Json</returns>
        public ResponsePayload FilterSerialJson(RequestPayload serialJson)
        {
            var respPayload = new ResponsePayload();
            var filteredJsonList = new List<SerialDetails>();

            try
            {
                // Check if the json is not null
                if (serialJson != null)
                {
                    // Iterate through Serial json
                    foreach (var serial in serialJson.Payload)
                    {
                        // If Drm is true and Episode count is greater than 0, add serial details to response
                        if (serial != null && serial.Drm && serial.EpisodeCount > 0)
                        {
                            filteredJsonList.Add(new SerialDetails()
                            {
                                Image = serial.Image != null ? serial.Image.ShowImage : "",
                                Slug = serial.Slug,
                                Title = serial.Title
                            });
                        }
                    }

                    respPayload.Response = filteredJsonList;
                }
                else {
                    return null;
                }
            }
            catch (Exception ex) {
                throw ex;    
            }

            // Return filtered list
            return respPayload;
        }
    }
}
