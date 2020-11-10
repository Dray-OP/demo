var user = {
    //phương thức
    init: function () {
        user.registerEvents();
    },
    //method
    registerEvents: function () {
        //off để k chạy vòng for trong .net
        $('.btn-active').off('click').on('click', function (e) {
            // để reset thẻ a trong sự kiện click
            e.preventDefault(); 
            debugger
            //
            var btn = $(this);
            var id = btn.data('id');

            $.ajax({
                url: "/Admin/User/ChangeStatus",
                // dữ liệu chuyển vào changeStatus
                data: { id: id },
                dataType:"Json",
                type: "Post",
                success: function (response) {
                    //if (response == true)
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            })

        })
    }
}
user.init();