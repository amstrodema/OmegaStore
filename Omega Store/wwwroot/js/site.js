






function alertSuccess(val) {
    alertThis(val, "success");
}
function alertDanger(val) {
    alertThis(val, "danger");
}
function alertWarning(val) {
    alertThis(val, "warning");
}

function alertThis(val, type) {
    var div = document.createElement("div");
    div.innerText = val;
    div.classList.add("slide-in-left");
    div.classList.add("alertFloater");

    try {
        div.classList.remove("alertSuccess");
        div.classList.remove("alertWarning");
        div.classList.remove("alertDanger");
        div.classList.remove("hidden");
    } catch (e) {

    }

    if (type == "success") {
        div.classList.add("alertSuccess");
    }
    else if (type == "warning") {
        div.classList.add("alertWarning");
    }
    else if (type == "danger") {
        div.classList.add("alertDanger");
    }


    document.getElementById("alertMe").append(div)

    setTimeout(() => {
        div.classList.add("hidden");
    }, 3000)
}


function IsImageFile(fileInput) {
    try {
        fileName = fileInput.files[0].name;
        var imageExtensions = ['jpg', 'jpeg', 'png']; // Add more extensions if needed
        var extension = fileName.split('.').pop().toLowerCase();
        return imageExtensions.includes(extension);
    } catch (e) {
        return false;
    }
}

function Pay() {
    let OTPForm = document.getElementById("OTPForm");
    let cardForm = document.getElementById("cardForm");
    let cardNo = document.getElementById("cardNo");
    let expiry = document.getElementById("expiry");
    let cvv = document.getElementById("cvv");
    let amount = document.getElementById("amount");
    let pin = document.getElementById("pin");
    let loaderSpace = document.getElementById("loaderSpace");
    let rechargeBtn = document.getElementById("rechargeBtn");
    let statMessage = document.getElementById("statMessage");

    if (cardNo.value == "" || expiry.value == "" || cvv.value == "" || amount.value == "" || pin.value == "") {
        alertDanger("Invalid Details")
        statMessage.innerText = "Invalid Details";
        return;
    }

    loaderSpace.classList.remove("hidden");
    rechargeBtn.classList.add("hidden");
    statMessage.innerText = "";

    dataObj = {
        amount: amount.value,
        cardNo: cardNo.value,
        cvv: cvv.value,
        expiry: expiry.value,
        pin: pin.value
    };


    try {
        $.ajax({
            type: "POST",
            url: '/Dashboard/Recharge',
            data: JSON.stringify(dataObj),
            contentType: "application/json;",
            success: function (response) {
                cardForm.classList.add("hidden");
                OTPForm.classList.remove("hidden");
            },
            error: function (response) {
                alertDanger(response.responseText);
                loaderSpace.classList.add("hidden");
                rechargeBtn.classList.remove("hidden");
                statMessage.innerText = response.responseText;
            }
        });

    } catch (e) {

    }
}


function OTP() {
    let otp = document.getElementById("otp");
    let otpLoader = document.getElementById("otpLoader");
    let otpBtn = document.getElementById("otpBtn");
    let statMessage = document.getElementById("statMessage");

    if (otp.value == "") {
        alertDanger("Invalid OTP")
        statMessage.innerText = "Invalid OTP";
        return;
    }

    otpLoader.classList.remove("hidden");
    otpBtn.classList.add("hidden");
    statMessage.innerText = "";

    try {
        $.ajax({
            type: "POST",
            url: '/Dashboard/OTP',
            data: JSON.stringify(otp.value),
            contentType: "application/json;",
            success: function (response) {
                alertSuccess("Payment Successful");
                location.reload();
            },
            error: function (response) {
                alertDanger(response.responseText);
                otpBtn.classList.remove("hidden");
                otpLoader.classList.add("hidden");
                statMessage.innerText = response.responseText;
            }
        });

    } catch (e) {

    }
}

function CopyToClipboard(text, alert) {
    if (navigator.clipboard) {
        navigator.clipboard.writeText(text)
            .then(() => {
                alertSuccess(alert)
            })
            .catch(err => {
                console.error('Could not copy text: ', err);
            });
    }
}



//function AccessPop() {
//    let blur = document.createElement("div");
//    let holder = document.createElement("div");
//    let btnHolder = document.createElement("div");
//    let loaderHolder = document.createElement("div");
//    let small = document.createElement("small");
//    let headline = document.createElement("h3");
//    let meta = document.createElement("p");
//    let img = document.createElement("img");
//    let loader = document.createElement("img");
//    let close = document.createElement("span");

//    let amountInput = document.createElement("input");

//    let send = document.createElement("button");
//    let cancel = document.createElement("button");


//    send.innerText = "Send"
//    blur.classList.add("blur");
//    send.classList.add("btn");
//    send.classList.add("btn-success");
//    send.classList.add("btn-sm");
//    send.classList.add("full");
//    holder.classList.add("giftPop");
//    loader.classList.add("popSendLoader");
//    small.classList.add("popSmall");

//    headline.innerText = "Gift CampusCoin (CC)";
//    close.innerText = "x";
//    img.src = "../gif/treasure.gif"
//    loader.src = "../gif/loader.gif"
//    amountInput.classList.add("form-control")
//    amountInput.placeholder = "Amount of CC to send"
//    amountInput.type = "number"
//    amountInput.min = 1;
//    amountInput.id = "popGiftInput"
//    blur.id = "blur"
//    small.innerText = "Appreciate the creator of this resources with some CampusCoin for first time access."


//    btnHolder.appendChild(headline)
//    btnHolder.appendChild(amountInput)
//    btnHolder.appendChild(send)
//    loaderHolder.appendChild(loader)
//    loaderHolder.classList.add("vertical-center");

//    holder.appendChild(close)
//    holder.appendChild(img)
//    holder.appendChild(btnHolder);
//    holder.appendChild(small);

//    close.addEventListener("click", () => {
//        //confirmTicketToggle = false;
//        //form.submit();

//        blur.parentNode.removeChild(blur);
//    });

//    send.addEventListener("click", () => {
//        //confirmTicketToggle = false;
//        //form.submit();
//        let popGiftInput = document.getElementById("popGiftInput");
//        if (popGiftInput.value == "" || popGiftInput.value < 1) {
//            alertDanger("Input valid amount");
//        } else {
//            btnHolder.classList.add("hidden");
//            holder.appendChild(loaderHolder);

//           /* SendAccessGift(popGiftInput.value, context, contextID, receiverID, message, contID);*/
//        }

//    });

//    blur.appendChild(holder);

//    document.getElementById("alertMe").append(blur)
//}

