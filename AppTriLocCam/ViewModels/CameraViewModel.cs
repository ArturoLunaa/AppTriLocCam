using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTriLocCam.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        //Comandos
        private Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        //Propiedades
        private string _Picture;

        public string Picture 
        { 
            get => _Picture;
            set => SetProperty(ref _Picture, value);       
        }

        //Constructor
        public CameraViewModel()
        {

        }
        //Métodos
        private async void TakePictureAction()
        {
            //Inicializa la cámara
                await CrossMedia.Current.Initialize();

            //Si la camara no está disponible o no está soportada lanza un mensaje y termina este método
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                //Toma la fotografía
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "AppTriLocCam",
                    Name = "cam_picture.jpg"
                });

                //Si el objeto file es null termina este método
                if (file == null)
                    return;

                //Asignamos la ruta de la fotografóa en la propiedad de la imagen
                Picture = file.Path;
               
                /*image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });*/

        }
    }
}
