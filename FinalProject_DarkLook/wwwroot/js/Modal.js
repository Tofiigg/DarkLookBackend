////$(document).ready(function () {

////    $(".show-product-modal").on("click", function () {
////        let id = $(this).attr("data-id")
////        fetch("http://localhost:44369/shop/getdetail/" + id)
////            .then(res => res.text())
////            .then(data => {
////                $(".modal-data-body").html(data)
////                $('.sliders5').slick({
////                    prevArrow: '',
////                    nextArrow: '',
////                    infinite: false,
////                    speed: 300,
////                    slidesToShow: 5,
////                    slidesToScroll: 1,
////                    autoplay: true,
////                    autoplaySpeed: 1200,
////                    responsive: [
////                        {
////                            breakpoint: 1024,
////                            settings: {
////                                slidesToShow: 2,
////                                slidesToScroll: 1,
////                                infinite: true,
////                                dots: true
////                            }
////                        },
////                        {
////                            breakpoint: 968,
////                            settings: {
////                                slidesToShow: 2,
////                                slidesToScroll: 1
////                            }
////                        },
////                        {
////                            breakpoint: 520,
////                            settings: {
////                                slidesToShow: 2,
////                                slidesToScroll: 1
////                            }
////                        }
////                    ]
////                });
////                let colors = document.querySelectorAll('.check-color');
////                colors.forEach(color => {
////                    color.addEventListener('click', function (e) {
                        
////                    e.preventDefault();
////                        let url = this.getAttribute('href');
////                        let colorId = color.getAttribute('data-colorId');
////                        let addtoCartUrl = document.querySelector('.info_pro1').getAttribute('href');
////                        let colorIdPart = addtoCartUrl.slice(21, 30);
////                        addtoCartUrl = addtoCartUrl.replace(colorIdPart, `colorId=${+colorId}`);
////                        document.querySelector('.info_pro1').setAttribute('href', addtoCartUrl);
////                        fetch(url).then(x => x.json()).then(colorsP => {
////                            let images = colorsP.productImages;
////                            let sizes = colorsP.productColorSizes;
////                            let sizeList = document.querySelector('.size-list');
////                            sizeList.innerHTML = '';
////                            sizes.forEach(size => {
////                                let li = document.createElement('li');
////                                li.setAttribute('data-sizeId', size.size.id);
////                                li.addEventListener('click', function (e) {
////                                    let sizeId = li.getAttribute('data-sizeId');
////                                    let sizeIdPart = addtoCartUrl.slice(31, 39);
////                                    addtoCartUrl = addtoCartUrl.replace(sizeIdPart, `sizeId=${+sizeId}`);
////                                    document.querySelector('.info_pro1').setAttribute('href', addtoCartUrl);
////                                    e.preventDefault();
////                                    document.getElementsByClassName("active")[0].classList.remove("active")
////                                    this.classList.add('active');
////                                    let info_pro1s = document.querySelectorAll(".info_pro1");
////                                    info_pro1s.forEach(info_pro1 =>
////                                    {
////                                        info_pro1.addEventListener("click", function (e) {
////                                            e.preventDefault();

////                                            $.ajax({
////                                                type: "GET",
////                                                url: this.getAttribute("href"),
////                                                data: {},
////                                                contentType: "application/json; charset=utf-8",
////                                                dataType: "json",
////                                                success: function (response) {

////                                                    // Looping over emloyee list and display it  
////                                                    $.each(response, function (index, emp) {
////                                                        $('#output').append('<p>Id: ' + emp.ID + '</p>' +
////                                                            '<p>Id: ' + emp.Name + '</p>');
////                                                    });
////                                                },
////                                            });
////                                        })
////                                    })

////                                    })
////                                li.innerText = size.size.name;
////                                //sizeList.innerHTML += `<li class="active">${size.size.name}</li>`;
////                                sizeList.append(li);

////                                let lists = document.querySelectorAll('.size-list li');

////                            });

////                            document.getElementById('myimage').setAttribute('src', `../assest/images/${images[0].image}`);
////                        });
////                });

////                })


////                $(".spinner-prev").click(function () {
////                    var prev = parseInt($(".spinner input").val());
////                    /*    console.log(prev);*/
////                    if (prev == 1) {
////                        $(".spinner input").val('1');

////                    }
////                    else {
////                        var prevVal = prev - 1;
////                        $(".spinner input").val(prevVal);

////                    }

////                });

////                document.querySelector('input.number-spinner').addEventListener('change', function () {
////                    let val = this.value;
////                    let addtoCartUrl = document.querySelector('.info_pro1').getAttribute('href');
////                    let countPart = addtoCartUrl.slice(40, 48);
////                    addtoCartUrl=addtoCartUrl.replace(countPart, `count=${val}`);
////                    console.log(addtoCartUrl);
////                    document.querySelector('.info_pro1').setAttribute('href', addtoCartUrl);
////                });


////                // Modal Cart decrease

////                $(".spinner-next").click(function () {
////                    var next = parseInt($(".spinner input").val());
////                    // console.log(next);
////                    if (next >= 100) {
////                        $(".spinner input").val('100');

////                    }
////                    else {
////                        var nextVal = next + 1;
////                        $(".spinner input").val(nextVal);

////                    }

////                });

////                $(".number-spinner").on("change", function () {
////                    var inputValue = parseInt($(".spinner input").val());
////                    if (inputValue >= 100) {
////                        $(".number-spinner").val("100");
////                    }
////                    else if (inputValue <= 0) {
////                        $(".number-spinner").val("0");
////                    }
////                });


////                ///Detail's tab button control

////                //On description Off review
////                $("#desc").click(function () {

////                    if (!$(this).hasClass("active")) {
////                        $(this).addClass("active");
////                        $("#review").removeClass("active");
////                        $(".reviews").addClass("d-none");
////                        $(".write-comment").addClass("d-none");
////                        $(".description").removeClass("d-none");
////                    }
////                });

////                //On review Off description

////                $("#review").click(function () {

////                    if (!$(this).hasClass("active")) {
////                        $(this).addClass("active");
////                        $("#desc").removeClass("active");
////                        $(".description").addClass("d-none");
////                        $(".reviews").removeClass("d-none");
////                        $(".write-comment").removeClass("d-none");
////                    }
////                });



////                //starts control


////                //Default image Detail
////                $(".imgBox img").attr("src", $(".mini-image-box li:first-child img").attr("src"));
////                //Get default product color
////                var defaultPrdColor = $('.mini-image-box li:first-child img').data("img-color");
////                //Set default color to button
////                $('.detail-prd-color').find('.' + defaultPrdColor).children().css({ "opacity": "1" });

////                $(".mini-img").click(function () {
////                    var value = $(this).attr("src");
////                    $(".imgBox img").attr("src", value);

////                    var btnBox = $(`.detail-prd-color`);

////                    var classValue = '.' + $(this).data("img-color");
////                    var btn = btnBox.find(classValue);

////                    $(".detail-prd-color span i").css({ "opacity": "0" });

////                    btn.children().css({ "opacity": "1" });


////                });

////                ///Change pictures by color control - Detail
////                $('.detail-prd-color span').click(function () {
////                    $(".detail-prd-color span i").css({ "opacity": "0" });
////                    var color = $(this).data("btn-color");

////                    if (color == "white") {
////                        $(this).find("i").css({ "opacity": "1", "color": "black" });
////                    }
////                    $(this).find("i").css({ "opacity": "1" });


////                    var currentImage = $(`[data-img-color="${color}"]`);
////                    $('.mini-img').each(function (index, img) {
////                        if (color == img.getAttribute('data-img-color')) {
////                            currentImage = img;
////                        }
////                    });
////                    /*  console.log(currentImage)*/
////                    //var currentImageSrc = currentImage.getAttribute("src");
////                    //$(".imgBox img").attr("src", currentImageSrc);


////                })

////                ///Change detail product size
////                $('.detail-prd-size ul li').click(function () {
////                    $('.detail-prd-size ul li').removeClass("active");
////                    $(this).addClass("active");
////                })
           

////            })
      
      
      
////    })
////})

////let colors = document.querySelectorAll('.check-color');

////colors.forEach(color => {
////    color.addEventListener('click', function (e) {

////        e.preventDefault();
////        let url = this.getAttribute('href');
////        fetch(url).then(x => x.json()).then(colorsP => {
////            let images = colorsP.productImages;
////            let sizes = colorsP.productColorSizes;
////            let sizeList = document.querySelector('.size-list');
////            /*    console.log(colorsP)*/
////            sizeList.innerHTML = ''; 
////            sizes.forEach(size => {
////                let li = document.createElement('li');

////                li.addEventListener('click', function (e) {
////                    e.preventDefault();
////                    document.getElementsByClassName("active")[0].classList.remove("active")
////                    this.classList.add('active');

////                })


////                li.innerText = size.size.name;
////                //sizeList.innerHTML += `<li class="active">${size.size.name}</li>`;
////                sizeList.append(li);

////                let lists = document.querySelectorAll('.size-list li');

////            });

////            document.getElementById('myimage').setAttribute('src', `/assest/images/${images[0].image}`);
////        });
////    });


////})
