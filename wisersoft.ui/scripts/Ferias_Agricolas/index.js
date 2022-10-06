var ferias

$(document).ready(function ()
{
    ferias = $('#txtferias').val().split('~')
    mostrarGoogleMaps(9.904713513651277, -83.68527918704831);	

})

function mostrarGoogleMaps(val1,val2)
{
	//Creamos el punto a partir de las coordenadas:
	punto = new google.maps.LatLng(val1, val2);
	//Configuramos las opciones indicando Zoom, punto(el que hemos creado) y tipo de mapa
	var myOptions = 
	{
		zoom: 7, center: punto, mapTypeId: google.maps.MapTypeId.ROADMAP
	};
	//Creamos el mapa e indicamos en qué caja queremos que se muestre
	var map = new google.maps.Map(document.getElementById("mapa"), myOptions);

	var image = {
	    url: '../images/feria.png',
	    // This marker is 20 pixels wide by 32 pixels high.
	    size: new google.maps.Size(32, 37),
	    // The origin for this image is (0, 0).
	    origin: new google.maps.Point(0, 0),
	    // The anchor for this image is the base of the flagpole at (0, 32).
	    anchor: new google.maps.Point(16, 34)
	};

    //Opcionalmente podemos mostrar el marcador en el punto que hemos creado.
	for (x = 0; x < ferias.length - 1; x++) {

	    valores = ferias[x].split(',')
	    //mostrarGoogleMaps(valores[1], valores[2], valores[0], valores[3])
	    punto = new google.maps.LatLng(valores[1], valores[2]);
        
	    var markup = new google.maps.Marker(
	    {
	        position: punto,
	        map: map,
	        title: valores[0]+" - "+valores[3],
	        icon: image
	    });

	    markup.addListener('click', function () {
	        var id = this.title.replace(" ", "").split('-')[0]
	        $(".row_feria").css("background-color","#fff")
	        $("#feria_" + id).css("background-color", "#ccc")


	    });

	}
	

	//marker.setAnimation(google.maps.Animation.BOUNCE);

	

}