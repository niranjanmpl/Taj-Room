
$(window).load(function () {
    var imgsrc;
    var hdrtxt;
    var txt;
    var ocrtext;
    setTimeout(function () {

        var maxh = $(document).height();

        var maxw = $(document).width();
        imgsrc = setimg();

        hdrtxt = setHdr();
        txt = setTxt();
        if (imgsrc != "None")
            $(".section").append('<img src=' + imgsrc + ' alt="" id="main_img" />');
        else
            $(".section").append('<img src="images/mainimg.jpg" alt="" id="main_img" />');


        //        hdrtxt = setHdr();
        //        ocrtext = setocrTxt();

        //        if (hdrtxt != "None" && txt != "None" && ocrtext == "None") {
        if (hdrtxt != "None" || txt != "None") {
            $(".section").append('<div class="strip1"> <h1>' + hdrtxt.replace("None", "") + '</h1> <h2>' + txt.replace("None", "") + '</h2> </div>');


        }
        var nav = $("#nav").outerHeight();

        var section = $(".section").outerWidth();

        var section1 = $('.section').height(nav);


        var heightsetter = function () {

            $('.section img').height(nav);

            $('.section img').width(section);

        }

        if (maxw <= 480) {

            $('.section img').css("height", "auto");
            $('.section img').width(section);


        }

        else {

            heightsetter();

        }



    }, 1);

});




function setimg() {

    var jsSrc = document.getElementById("imgsrc").src.split("src=")[1];
    jsSrc = jsSrc.split("?header=")[0];
    jsSrc = jsSrc.replace(/%27/g, "");



    //    if (jsSrc == "None") {
    //        jsSrc = "images/mainimg.jpg";
    //    }
    //    
    return jsSrc;
}

function setHdr() {

    var jsSrc = document.getElementById("imgsrc").src.split("header=")[1];
    jsSrc = jsSrc.split("?text=")[0];
    jsSrc = jsSrc.replace(/%27/g, "");
    if (jsSrc == "") {
        jsSrc = "None";
    }

    jsSrc = jsSrc.replace(/%20/g, " ");
    return jsSrc;
} 

function setTxt() {

    var jsSrc = document.getElementById("imgsrc").src.split("text=")[1];
    jsSrc = jsSrc.replace(/%27/g, "");
    if (jsSrc == "") {
        jsSrc = "None";
    }

    jsSrc = jsSrc.replace(/%20/g, " ");
    return jsSrc;
}


//function setocrTxt() {
//    
//    var jsSrc = document.getElementById("imgsrc").src.split("ocrtext=")[1];
//   
//    jsSrc = jsSrc.replace(/%27/g, "");
//    //    if (jsSrc == "None") {
//    //        jsSrc = "Welcome to Taj Hotels";
//    //    }

//    jsSrc = jsSrc.replace(/%20/g, " ");
//    return jsSrc;
//}
