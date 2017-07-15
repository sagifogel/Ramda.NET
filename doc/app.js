var fs = require("fs");
var babel = require("babel-core");
var doctrine = require("doctrine");

babel.transformFile("ramda.js", { ast: true }, function (err, result) {
    if (err) {
        return console.log(err);
    }

    var expressions = result.ast.program.body
                                        .filter(function (s) { return s.type === "ExpressionStatement"; })
                                        .map(function (s) { return s.expression.callee.object.body.body; })
                                        .reduce(function (ex1, ex2) { return ex1.concat(ex2) }, []);

    var props = expressions.filter(function (ex) {
        if (ex.declarations && ex.declarations.length > 0) {
            var declaration = ex.declarations[0];

            if (ex.type === "VariableDeclaration" && declaration.type === "VariableDeclarator" && declaration.id && declaration.id.name === "R") {
                return true;
            }
        }

        return false;
    }).map(function (ex) { return ex.declarations[0].init.properties; })[0].map(function (p) { return p.key.name; });

    expressions = expressions.filter(function (ex) {
        if (ex.declarations && ex.declarations.length > 0) {
            var declaration = ex.declarations[0];

            return ex.type === "VariableDeclaration" &&
                   declaration.type === "VariableDeclarator" &&
                   declaration.init.type !== "ObjectExpression" &&
                   declaration.id && props.indexOf(declaration.id.name) > -1;
        }

        return false;
    });

    expressions = expressions.map(function (ex) { return { name: ex.declarations[0].id.name, leadingComments: doctrine.parse(ex.leadingComments.map(function (c) { return c.value; }).join(""), { unwrap: true }) } });
    fs.writeFile("doc.js", JSON.stringify(expressions), function (err) {
        process.exit();
    });
});
