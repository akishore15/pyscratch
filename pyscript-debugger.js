class PyScratchDebugger {
    constructor(runtime) {
        this.runtime = runtime;
        this._isVisible = false;
        this._createDebuggerUI();
    }

    _createDebuggerUI() {
        const debuggerDiv = document.createElement('div');
        debuggerDiv.id = 'pyscratch-debugger';
        debuggerDiv.style.position = 'fixed';
        debuggerDiv.style.bottom = '10px';
        debuggerDiv.style.right = '10px';
        debuggerDiv.style.width = '300px';
        debuggerDiv.style.height = '400px';
        debuggerDiv.style.backgroundColor = 'white';
        debuggerDiv.style.border = '1px solid black';
        debuggerDiv.style.overflowY = 'scroll';
        debuggerDiv.style.display = this._isVisible ? 'block' : 'none';

        const toggleButton = document.createElement('button');
        toggleButton.innerText = 'Toggle Debugger';
        toggleButton.onclick = () => {
            this._isVisible = !this._isVisible;
            debuggerDiv.style.display = this._isVisible ? 'block' : 'none';
        };

        document.body.appendChild(toggleButton);
        document.body.appendChild(debuggerDiv);

        this.debuggerDiv = debuggerDiv;
    }

    log(message) {
        const logEntry = document.createElement('div');
        logEntry.innerText = message;
        this.debuggerDiv.appendChild(logEntry);
        this.debuggerDiv.scrollTop = this.debuggerDiv.scrollHeight;
    }
}

// Register the extension with the Scratch runtime
(function (Scratch) {
    const extensionClass = PyScratchDebugger;
    Scratch.extensions.register(new extensionClass(Scratch.vm.runtime));
})(window.Scratch);
