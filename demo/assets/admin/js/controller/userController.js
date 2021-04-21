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
        $('.btn-edit').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/User/FindEmpoyee',
                data: { id: id },
                dataType: "Json",
                type: "Post",
                success: function (res) {
                    $('#userName').val(res.UserName) 
                    $('#name').val(res.Name) 
                    $('#address').val(res.Address) 
                    $('#email').val(res.Email) 
                    $('#phone').val(res.phone)
                    $('#id').val(res.ID) 
                }
            })
        })
        $('.confirm-edit').off('click').on('click', function (e) {
            e.preventDefault();
            debugger
            let formArray = $('form.form-input').serializeArray();
            var object = {};
            jQuery.map(formArray, function (n, i) {
                object[n.name] = n.value;
            });
            console.log(object);

            $.ajax({
                url: '/Admin/User/EditEmpoloyee',
                data: { user: object },
                dataType: "Json",
                type: "Post",
                success: function (user) {
                    let id = user.ID;
                    $("#row_" + id).children('.userName').text(user.UserName)
                    console.log(user)
                }
            })
        })
    }
}
user.init();