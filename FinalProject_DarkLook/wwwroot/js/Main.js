



    $(document).on("keyup", "#searchProduct", function (e) {
        e.preventDefault();

        let search = $(this).val();

        $("#searchedList").empty();

        if (search.length > 0) {
            let url = "/product/search?search=" + search;
            fetch(url).then(response => response.text())
                .then(data =>
                {
                    $("#searchedList").html(data);
                });
        }
    })

    
    