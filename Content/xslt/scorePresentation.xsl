<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:saves="http://example.com/saves"
                version="1.0">
    <xsl:template match="/">
        <html>
            <head>
                <title>HighScores</title>
            </head>
            <body>
                <h1>High-Scores:</h1>
                <xsl:for-each select="//saves:save">
                    <xsl:sort select="@score" data-type="number" order="descending"/>
                    <h2>Score: <xsl:value-of select="@score"/> Player:<xsl:value-of select="@player"/> </h2>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>