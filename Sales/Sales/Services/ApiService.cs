namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Common.Models;
    using Helpers;
    using System.Text;
    using System.Net.Http.Headers;

    public class ApiService
    {  
        // para validar conexion
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.TurnOnInternet,
                };

            }
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.NoInternet,
                };
            }

            return new Response
            {
                IsSuccess = true,
            };


        }

        public async Task<TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }

        // Nos sirve para consumir cualquier servicio api y cualquier lista es decir crea una lista de caulquier cosa
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)  // metodo para la comunicacion restfull
        {
            try
            {
                // forma de consumir un servicio
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                var url = $"{prefix}{controller}"; // prefijo concatenado
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                // deserialisar convertir string a objetos
                // serializar operasion inversa

                var list = JsonConvert.DeserializeObject<List<T>>(answer); 
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                
            }
        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller, string tokenType, string accessToken)  // metodo para la comunicacion restfull
        {
            try
            {
                // forma de consumir un servicio
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}"; // prefijo concatenado
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                // deserialisar convertir string a objetos
                // serializar operasion inversa

                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller, int id, string tokenType, string accessToken)  // metodo para la comunicacion restfull
        {
            try
            {
                // forma de consumir un servicio
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}/{id}"; // prefijo concatenado
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                // deserialisar convertir string a objetos
                // serializar operasion inversa

                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                // forma de consumir un servicio
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                var url = $"{prefix}{controller}"; // prefijo concatenado
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                // deserialisar convertir string a objetos
                // serializar operasion inversa

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model, string tokenType, string accessToken)
        {
            try
            {
                // forma de consumir un servicio
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}"; // prefijo concatenado
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                // deserialisar convertir string a objetos
                // serializar operasion inversa

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> Put<T>(string urlBase, string prefix, string controller, T model, int id)
        {
            try
            {
                // forma de consumir un servicio
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                var url = $"{prefix}{controller}/{id}"; // prefijo concatenado
                var responce = await client.PutAsync(url, content);
                var answer = await responce.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!responce.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                // deserialisar convertir string a objetos
                // serializar operasion inversa

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> Put<T>(string urlBase, string prefix, string controller, T model, int id, string tokenType, string accessToken)
        {
            try
            {
                // forma de consumir un servicio
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}/{id}"; // prefijo concatenado
                var responce = await client.PutAsync(url, content);
                var answer = await responce.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!responce.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                // deserialisar convertir string a objetos
                // serializar operasion inversa

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> Delete(string urlBase, string prefix, string controller, int id)  // metodo para la comunicacion restfull
        {
            try
            {
                // forma de consumir un servicio
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                var url = $"{prefix}{controller}/{id}"; // prefijo concatenado
                var responce = await client.DeleteAsync(url);
                var answer = await responce.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!responce.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }

                return new Response
                {
                    IsSuccess = true,

                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> Delete(string urlBase, string prefix, string controller, int id, string tokenType, string accessToken)  // metodo para la comunicacion restfull
        {
            try
            {
                // forma de consumir un servicio
                var client = new HttpClient();   // este objeto client establece la comunicacion
                client.BaseAddress = new Uri(urlBase); // direccion url
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}/{id}"; // prefijo concatenado
                var responce = await client.DeleteAsync(url);
                var answer = await responce.Content.ReadAsStringAsync(); // leer respuesta del servicio queda un Json en formato streeng
                if (!responce.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }

                return new Response
                {
                    IsSuccess = true,

                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<Response> GetUser(string urlBase, string prefix, string controller, string email,
 string tokenType, string accessToken)
        {
            try
            {
                var getUserRequest = new GetUserRequest
                {
                    Email = email,
                };
                var request = JsonConvert.SerializeObject(getUserRequest);
                
            var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType,
                accessToken);
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                var user = JsonConvert.DeserializeObject<MyUserASP>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = user,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<FacebookResponse> GetFacebook(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.8/me/?fields=name," +
            "picture.width(999),cover,age_range,devices,email,gender," +
            "is_verified,birthday,languages,work,website,religion," +
            "location,locale,link,first_name,last_name," +
            "hometown&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookResponse =
            JsonConvert.DeserializeObject<FacebookResponse>(userJson);
            return facebookResponse;
        }
        public async Task<InstagramResponse> GetInstagram(string accessToken)
        {
            var client = new HttpClient();
            var userJson = await client.GetStringAsync(accessToken);
            var InstagramJson = JsonConvert.DeserializeObject<InstagramResponse>(userJson);
            return InstagramJson;
        }

        public async Task<TokenResponse> LoginTwitter(string urlBase, string servicePrefix, string controller,
TwitterResponse profile)
        {
            try
            {
                var request = JsonConvert.SerializeObject(profile);
                var content = new StringContent(
                request,
                Encoding.UTF8,
                "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                
            var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var tokenResponse = await GetToken(
                urlBase,
                profile.IdStr,
                profile.IdStr);
                return tokenResponse;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TokenResponse> LoginInstagram(string urlBase, string servicePrefix, string
        controller, InstagramResponse profile)
        {
            try
            {
                var request = JsonConvert.SerializeObject(profile);
                var content = new StringContent(
                request,
                Encoding.UTF8,
                "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var tokenResponse = await GetToken(
                urlBase,
                profile.UserData.Id,
                profile.UserData.Id);
                return tokenResponse;
                
            }
            catch
            {
                return null;
            }
        }

        public async Task<TokenResponse> LoginFacebook(string urlBase, string servicePrefix, string
        controller, FacebookResponse profile)
        {
            try
            {
                var request = JsonConvert.SerializeObject(profile);
                var content = new StringContent(
                request,
                Encoding.UTF8,
                "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var tokenResponse = await GetToken(
                urlBase,
                profile.Id,
                profile.Id);
                return tokenResponse;
            }
            catch
            {
                return null;
            }
        }

    }
}



    


