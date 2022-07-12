

const button=document.getElementById('button')
button.addEventListener('click',() =>{
    let xhr 
    if(window.XMLHttpRequest)
       xhr=new XMLHttpRequest()
    else   xhr=new ActiveXObject("Microsoft.XMLHTTP")

    xhr.open('GET','http://190.104.2.126/ServicioWebPrueba/api/verNombres')
xhr.addEventListener('load',(data) => {
console.log(JSON.parse(data.target.reponse))
})
    xhr.send(null);

    })