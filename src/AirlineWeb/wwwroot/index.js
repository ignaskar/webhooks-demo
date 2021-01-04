let registerButton = document.querySelector("#register");
let webhookURI = document.querySelector("#webhook");
let webhookType = document.querySelector("#webhooktype");
let successBox = document.querySelector("#alertSuccess");
let dangerBox = document.querySelector("#alertDanger");
let dangerMessage = document.querySelector("#dangerMessage");
let successMessage = document.querySelector("#successMessage");

successBox.style.display = 'none';
dangerBox.style.display = 'none';

registerButton.onclick = () => {
    successBox.style.display = 'none';
    dangerBox.style.display = 'none';

    if (webhookURI.value === "") {
        dangerMessage.innerHTML = "Please enter a URI";
        dangerBox.style.display = 'block';
    } else {
        (async () => {
            const rawResponse = await fetch('https://localhost:5001/api/v1/WebhookSubscription',
                {
                    method: 'POST',
                    body: JSON.stringify(
                        {
                            webhookUri: webhookURI.value,
                            webhookType: webhookType.value,
                        }),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                });
            
            const content = await rawResponse.json();

            successMessage.innerHTML =
                "Webhook Registered! Please use secret: " + content.secret + " to validate inbound request";

            successBox.style.display = 'block';
        })();
    }
}