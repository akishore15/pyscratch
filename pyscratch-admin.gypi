print("PyScratch core. If you would like to view PyScratch, please visit: \n https://github.com/akishore15/pyscratch")
print("Or you can login.")
print("Login")
username = input("Username: \n")
password = input("Password: \n")
if username == "admin" and password == "password":
    print("Welcome to the PyScript database. Here is the list of the following files:")
    print("styles.css (CSS Source File), index.html (HTML Source File), docs.html (HTML Source File), core.pyt (Python Source File).")
    addend = input("Add; choose a addend to add.")
    goto = f"{addend} will be requested..."
    print(goto)
    print(f"Go to: {addend}")
    for x in range(input("Choose an amount of files to be requested."):
        print("Add here:")
        input("\n")
    
else:
    import time
    code = "invalid-username-or-password"
    print(code)
    time.sleep(3)
    exit()
