<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" version="1.0">
  <xsl:template match="/hello-world">
    <html>
      <head>
        <title></title>
      </head>
      <body>
        <h1>
          <xsl:value-of select="greeting"/>
        </h1>
        <xsl:apply-templates select="greeter"/>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="greeter">
    <div>from <i><xsl:value-of select="."/></i></div>
  </xsl:template>
</xsl:stylesheet>
