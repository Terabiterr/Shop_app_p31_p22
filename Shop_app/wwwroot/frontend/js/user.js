async function login() {
    const email = document.getElementById("emailId").value;
    const password = document.getElementById("passwordId").value;
    if (!email || !password) {
        throw new Error(`email or password are ampty ...`);
    }
    await fetch(
        "https://localhost:7089/api/apiuser/auth",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "email": email,
                "password": password
            })
        }).then(response => {
            if (!response.ok) {
                throw new Error(`Error auth: ${response.status}`)
            }
            return response.json()
        }).then(data => {
            localStorage.setItem("token", data.token.result)
            console.log(data.token.result)
            return data.token.result;
        }).catch(err => console.log(err))
}
async function register(email, password) {
    try {
        const response = await fetch(
            "https://localhost:7089/api/apiuser/register",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    "email": email,
                    "password": password
                })
            });
        if (!response.ok) {
            throw new Error(`Error auth: ${response.status}`)
        }
        const data = await response.json()
        return data.token;
    } catch (error) {
        console.error(`Error: ${error}`)
    }
}