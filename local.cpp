#include <boost/beast/core.hpp>
#include <boost/beast/http.hpp>
#include <boost/beast/version.hpp>
#include <boost/asio/ip/tcp.hpp>
#include <boost/config.hpp>
#include <iostream>
#include <string>
#include <filesystem>

namespace beast = boost::beast;
namespace http = beast::http;
namespace net = boost::asio;
using tcp = net::ip::tcp;
namespace fs = std::filesystem;

void handle_request(const http::request<http::string_body>& req, http::response<http::string_body>& res) {
    std::string target = req.target().to_string();
    if (target == "/")
        target = "/index.html";
    
    fs::path path = fs::current_path() / "repo_files" / target;
    
    if (!fs::exists(path)) {
        res = http::response<http::string_body>(http::status::not_found, req.version());
        res.set(http::field::server, "Boost.Beast");
        res.set(http::field::content_type, "text/plain");
        res.body() = "File not found\r\n";
        res.prepare_payload();
        return;
    }

    std::ifstream file(path.string(), std::ios::in | std::ios::binary);
    std::stringstream buffer;
    buffer << file.rdbuf();

    res = http::response<http::string_body>(http::status::ok, req.version());
    res.set(http::field::server, "Boost.Beast");
    res.set(http::field::content_type, "text/html");
    res.body() = buffer.str();
    res.prepare_payload();
}

int main() {
    try {
        auto const address = net::ip::make_address("127.0.0.1");
        unsigned short port = 8080;

        net::io_context ioc{1};

        tcp::acceptor acceptor{ioc, {address, port}};
        for (;;) {
            tcp::socket socket{ioc};
            acceptor.accept(socket);

            beast::flat_buffer buffer;
            http::request<http::string_body> req;
            http::read(socket, buffer, req);

            http::response<http::string_body> res;
            handle_request(req, res);

            http::write(socket, res);
            socket.shutdown(tcp::socket::shutdown_send);
        }
    } catch (std::exception& e) {
        std::cerr << "Error: " << e.what() << std::endl;
        return EXIT_FAILURE;
    }
}
