function pokaziSakrij(objectId) {
    var obj = document.getElementById(objectId);
    if (obj.style.display == 'none')
        obj.style.display = 'block';
    else
        obj.style.display = 'none';
    //return false;
}

function UcitajPosao(currPosaoId, lite) {
    var client = new XMLHttpRequest();
    var response = '';
    client.onreadystatechange = function () {
        if (client.readyState == 4 && client.status == 200) {
            //document.getElementById("poslovi").innerHTML = posloviDiv.innerHTML + client.responseText;
            posloviPretraga();
            response = client.responseText;
        }
    };
    if (typeof lite !== "undefined")
        client.open("GET", "/Posao/DajPosaoPartialView?posaoId=" + currPosaoId + '&lite=' + lite, false);
    else
        client.open("GET", "/Posao/DajPosaoPartialView?posaoId=" + currPosaoId, false);
    client.send();
    return response;
}

function materijaliPretraga(searchBox) {
    var materijaliDiv = document.getElementById('materijaliDiv');

    var prviStepen = materijaliDiv.children;
    for (var i = 0; i < prviStepen.length; i = i + 1) {
        var drugiStepen = prviStepen[i].getElementsByTagName('div')[0];
        var treciStepen = drugiStepen.getElementsByTagName('div')[0];
        var cetvrtiStepen = treciStepen.getElementsByTagName('h3')[0];

        if (cetvrtiStepen.textContent.toUpperCase().indexOf(searchBox.value.toUpperCase()) >= 0) {
            prviStepen[i].style.display = '';
        } else {
            prviStepen[i].style.display = 'none';
        }
    }
}

function posloviPretraga() {
    var searchBox = document.getElementById('tekstZaPretragu');
    if (searchBox == null || typeof searchBox === "undefined")
        return;
    var posloviDiv = document.getElementById('poslovi');
    if (posloviDiv == null || typeof posloviDiv === "undefined")
        return;

    var prviStepen = posloviDiv.getElementsByTagName("div"); // children;
    for (var i = 0; i < prviStepen.length; i = i + 1) {
        var drugiStepen = prviStepen[i].getElementsByTagName('h4')[0];
        //var treciStepen = drugiStepen.getElementsByTagName('h4')[0];
        //var cetvrtiStepen = treciStepen.getElementsByTagName('h3')[0];

        if (drugiStepen.textContent.toUpperCase().indexOf(searchBox.value.toUpperCase()) >= 0) {
            prviStepen[i].style.display = '';
        } else {
            prviStepen[i].style.display = 'none';
        }
        UcitajPoslove();
    }
}

function UcitajSveMaterijale() {
    var vrsteMaterijalaDiv = document.getElementById('vrstaMaterijala');
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {

            if (xhttp.responseText == '') {
                return;
            }
            var materijali = JSON.parse(xhttp.responseText);
            for (var i = 0; i < materijali.length; i = i + 1) {
                var opt = document.createElement('option');
                opt.value = materijali[i].ID;
                opt.innerHTML = materijali[i].Naziv;
                vrsteMaterijalaDiv.appendChild(opt);
            }
        }
    };
    xhttp.open("GET", "/Materijali/DajSvejson", true);
    xhttp.send();
}

function ajaxRequest() {
    var activexmodes = ["Msxml2.XMLHTTP", "Microsoft.XMLHTTP"] //activeX versions to check for in IE
    if (window.ActiveXObject) { //Test for support for ActiveXObject in IE first (as XMLHttpRequest in IE7 is broken)
        for (var i = 0; i < activexmodes.length; i++) {
            try {
                return new ActiveXObject(activexmodes[i])
            }
            catch (e) {
                //suppress error
            }
        }
    }
    else if (window.XMLHttpRequest) // if Mozilla, Safari etc
        return new XMLHttpRequest()
    else
        return false
}

//Sample call:
//var myajaxrequest=new ajaxRequest()

function pokaziPoruku(poruka, tip) {
    var vrsteMaterijalaDiv = document.getElementById('vrstaMaterijala');
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {

            if (xhttp.responseText == '') {
                return;
            }
            var bodyDiv = document.getElementById('bodyDiv');
            var porukaDiv = document.createElement('div');
            porukaDiv.innerHTML = xhttp.response;
            bodyDiv.appendChild(porukaDiv);
            window.setTimeout(function () {
                bodyDiv.removeChild(porukaDiv);
            }, 3500);
        }
    };
    if (tip == 'obavjest')
        xhttp.open("GET", '/Home/GetMessageView?message=' + poruka, true);
    else
        xhttp.open("GET", '/Home/GetMessageView?tip=greska&message=' + poruka, true);
    xhttp.send();

}

function PreuzmiPosao(posaoId, korisnikId) {

    $.ajax({
        url: "/api/Posao1/PreuzmiPosao/" + posaoId + '/' + korisnikId,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ korisnikId: korisnikId }),
        success: function (data, textStatus, jqXHR) {
            pokaziPoruku('Posao preuzet!', 'obavjest');
            document.getElementById("poslovi").innerHTML = '';
            posaoId = 0;
            brojUcitavanja = 0;
            UcitajPoslove();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            pokaziPoruku(jqXHR.responseJSON.Message, 'greska');
        }
    });
}

function Zamagli() {
    document.getElementById('zamagljenaPozadina').style.display = 'block';
}

function Odmagli() {
    document.getElementById('zamagljenaPozadina').style.display = 'none';
    document.getElementById('sadrzajZamagljenePozadine').innerHTML = '';
}

function ZavrsiPosao(posaoId, korisnikId) {

    $.ajax({
        url: "/api/Posao1/ZavrsiPosao/" + posaoId + '/' + korisnikId,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        //data: JSON.stringify({ korisnikId: korisnikId }),
        success: function (data, textStatus, jqXHR) {
            pokaziPoruku('Posao zavrsen!', 'obavjest');
            document.getElementById("poslovi").innerHTML = '';
            
            UcitajPoslove(0);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            pokaziPoruku(jqXHR.responseJSON.Message, 'greska');
        }
    });
}

function PokaziPosloveZaKorisnika(id) {
    $.ajax({
        url: "/api/Posao1/DajPosloveZaKorisnika/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            Zamagli();
            var posloviDiv = document.getElementById('sadrzajZamagljenePozadine');
            var poslovi = JSON.parse(data.Message);

            for(var i =0; i<poslovi.length; i++){
                posloviDiv.innerHTML += UcitajPosao(poslovi[i].ID, true);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            pokaziPoruku(jqXHR.responseJSON.Message, 'greska');
        }
    });
}
function PokaziPosloveZaMaterijal(id) {
    $.ajax({
        url: "/api/Posao1/DajPosloveZaMaterijal/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            Zamagli();
            var posloviDiv = document.getElementById('sadrzajZamagljenePozadine');
            var poslovi = JSON.parse(data.Message);

            for (var i = 0; i < poslovi.length; i++) {
                posloviDiv.innerHTML += UcitajPosao(poslovi[i].ID, true);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            pokaziPoruku(jqXHR.responseJSON.Message, 'greska');
        }
    });
}