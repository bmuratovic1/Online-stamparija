function pokaziSakrij(objectId) {
    var obj = document.getElementById(objectId);
    if (obj.style.display == 'none')
        obj.style.display = 'block';
    else
        obj.style.display = 'none';
    //return false;
}