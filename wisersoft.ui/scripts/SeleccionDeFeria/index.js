var ferias
var cliente
var locations

$(document).ready(function ()
{
    ferias  = $('#txtferias' ).val().split('~')
    cliente = $('#txtcliente').val()

    /*var numero = 1;
    window["variable" + numero] = "valor";
    alert(window["variable" + numero]);*/

    obtenerPuntos();
})

function obtenerPuntos()
{
    locations = [];
    for (x = 0; x < ferias.length - 1; x++) 
    {
        valores = ferias[x].split(',')
        locations[x]= valores[1]+","+valores[2]
    }
    getDistance(cliente)
}


function getDistance(cliente)
{
        var distanceService = new google.maps.DistanceMatrixService();
        distanceService.getDistanceMatrix(
        {
            origins: [cliente],
            destinations: locations,
            travelMode: google.maps.TravelMode.DRIVING,
            unitSystem: google.maps.UnitSystem.METRIC,
            durationInTraffic: true,
            avoidHighways: false,
            avoidTolls: false
        },callback)

    
}

var contador = 0;

function callback(response, status) {
    if (status !== google.maps.DistanceMatrixStatus.OK) {
        console.log('Error:', status);
    } else {
        for (var i = 0; i < response.rows.length; i++)
        {
            for (var j = 0; j < response.rows[i].elements.length; j++)
            {
                var distance = response.rows[i].elements[j].distance.text;
                var duration = response.rows[i].elements[j].duration.text;
                ferias[j] = ferias[j]+","+duration+","+distance
            }
        }

        mostrarGoogleMaps(9.904713513651277, -83.68527918704831);
    }
};














function mostrarGoogleMaps(val1,val2)
{
	//Creamos el punto a partir de las coordenadas:
	punto = new google.maps.LatLng(val1, val2);
	//Configuramos las opciones indicando Zoom, punto(el que hemos creado) y tipo de mapa
	var myOptions = 
	{
		zoom: 8, center: punto, mapTypeId: google.maps.MapTypeId.ROADMAP
	};
	//Creamos el mapa e indicamos en qué caja queremos que se muestre
	var map = new google.maps.Map(document.getElementById("mapa"), myOptions);

    /*******************CASA CLIENTE************************/
	var imageCasa = {
	    url: '../images/casa.png',
	    // This marker is 20 pixels wide by 32 pixels high.
	    size: new google.maps.Size(32, 37),
	    // The origin for this image is (0, 0).
	    origin: new google.maps.Point(0, 0),
	    // The anchor for this image is the base of the flagpole at (0, 32).
	    anchor: new google.maps.Point(16, 34)
	};

    values = cliente.split(",")
	punto = new google.maps.LatLng(values[0],values[1]);
	var markup = new google.maps.Marker(
    {
        position: punto,
        map: map,
        title: "Tu hogar",
        icon: imageCasa
    })
    /*******************************************/

    /******************FERIAS AGRICOLAS*************************/
	var image = {
	    url: '../images/feria.png',
	    // This marker is 20 pixels wide by 32 pixels high.
	    size: new google.maps.Size(32, 37),
	    // The origin for this image is (0, 0).
	    origin: new google.maps.Point(0, 0),
	    // The anchor for this image is the base of the flagpole at (0, 32).
	    anchor: new google.maps.Point(16, 34)
	};

	var infowindow = new google.maps.InfoWindow();
	var markup,x;

	for (x = 0; x < ferias.length - 1; x++)
	{

	    valores = ferias[x].split(',')
	    //mostrarGoogleMaps(valores[1], valores[2], valores[0], valores[3])
	    punto = new google.maps.LatLng(valores[1], valores[2]);

	    /*var infowindow = new google.maps.InfoWindow({
	        content: contentString
	    });*/
	    

	    markup = new google.maps.Marker(
	    {
	        position: punto,
	        map: map,
	        title: valores[0]+" - "+valores[3],
	        icon: image
	    });

	    google.maps.event.addListener(markup, 'click', (function (markup, x) {
	        return function () {
	            valores = ferias[x].split(',');
	            var contentString = ''+
                    '<div id="content">' +
                        '<div id="siteNotice">' +
                        '</div>' +
                        '<h3 id="firstHeading" class="firstHeading">' + valores[3] + '</h3>' +
                        '<div id="bodyContent">' +
                            '<p><b>Distancia: </b>' + valores[5] + '<br><b>Duracion: </b>' + valores[4] + '<br></p>' +
                            '<input type="submit" value="Ir" class="btn btn-default" onClick="seleccionarFeria('+valores[0]+')"  />' +
                        '</div>' +
                    '</div>';
	            infowindow.setContent(contentString);
	            infowindow.open(map, markup);
	        }
	    })(markup, x));

	}
    /*******************************************/	

}

function seleccionarFeria(idFeria)
{
    $.ajax(
    {
        url: '/SeleccionDeFeria/Seleccion',
        type: "GET",
        dataType: "JSON",
        data: { idFeria: idFeria },
        success: function (va) {
            window.location = "/Compras/Index";
        }
        ,
        error: function (xhr, status, error) {
            alert('error')
        }
    });
}