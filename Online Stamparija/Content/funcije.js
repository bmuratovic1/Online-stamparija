function pokaziSakrij(objectId) {
    var obj = document.getElementById(objectId);
    if (obj.style.display == 'none')
        obj.style.display = 'block';
    else
        obj.style.display = 'none';
    //return false;
}

function materijaliPretraga(searchBox) {
    var materijaliDiv = document.getElementById('materijaliDiv');

    var prviStepen = materijaliDiv.children; // getElementsByTagName('div');
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