document.addEventListener('DOMContentLoaded', () => {
    session_user()
})

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
            localStorage.setItem("jwt_token", data.token.result)
            session_user()
            return data.token.result;
        }).catch(err => console.log(err))
}
async function register() {    
    const username = document.getElementById("username_register_id").value;
    const password = document.getElementById("password_register_id").value;
    try {
        const response = await fetch(
            "https://localhost:7089/api/apiuser/register",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    "email": username,
                    "password": password
                })
            });
        if (!response.ok) {
            throw new Error(`Error auth: ${response.status}`)
        }
        const data = await response.json()
        console.log(data)
    } catch (error) {
        console.error(`Error: ${error}`)
    }
}

function parseJwt(token) {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64).split('').map(c =>
        '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
      ).join('')
    );
    return JSON.parse(jsonPayload);
  } catch (e) {
    return null;
  }
}

async function session_user() {
    //Дізнатися з токену інформацію про користувача
    // Приклад використання:
    const token = localStorage.getItem('jwt_token');
    const payload = parseJwt(token);
    console.log('Це користувач:', payload);
    if(token) {
        var box_register = document.getElementById("box_register")
        box_register.style.display = "none";
        var box_login = document.getElementById("box_login")
        box_login.style.display = "none";
    }

}

async function logout() {
    localStorage.removeItem("jwt_token");
    window.location.reload()
}