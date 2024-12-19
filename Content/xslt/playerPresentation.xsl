<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:saves="http://example.com/saves"
                version="1.0">
    <xsl:param name="player" select="'player1'"/>
    <xsl:template match="/">
        <html>
            <head>
                <title>Player: <xsl:value-of select="$player"/></title>
            </head>
            <body>
                <h1>Player: <xsl:value-of select="$player"/></h1>
                <xsl:for-each select="//saves:save[@player=$player]">
                    <h2>Date: <xsl:value-of select="@date"/> Score: <xsl:value-of select="@score"/></h2>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>