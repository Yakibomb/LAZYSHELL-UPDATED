<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:output method="html" doctype-system="about:legacy-compat" />
  <xsl:template match="/">
    <html>
      <head>
        <link href="LAZYSHELL_css.css" type="text/css" rel="stylesheet"  />
        <script type="text/javascript">
          <![CDATA[
            var z = 0;
            function ShowHide(editor, faq) 
            {
                document.getElementById(editor).style.display = "block";
                document.getElementById(editor).style.zIndex = ++z;
                document.getElementById(faq).style.display = "block";
                document.getElementById(faq).style.zIndex = z;
            }
          ]]>
        </script>
      </head>
      <body>
        <!--TITLE-->
        <xsl:for-each select="LAZYSHELL">
          <a class="body" href="javascript:void(0)"
             onclick="ShowHide('{@form}', '{concat('faq_', @form)}')">
            <img src="{@icon}" title="LAZY SHELL - Help Database"/>
            LAZY SHELL - Help Database
          </a>
          <!--SHORTCUT SIDE BAR-->
          <div class="left"/>
          <div class="lefttabs">
            <xsl:for-each select="Editors/*[name() != 'header']">
              <xsl:call-template name="lefttabs"/>
            </xsl:for-each>
            <hr/>
            <xsl:for-each select="Other/*[name() != 'header']">
              <xsl:call-template name="lefttabs"/>
            </xsl:for-each>
          </div>
          <!--EDITORS-->
          <xsl:for-each select="Other/*[name() != 'header']">
            <xsl:call-template name="editor"/>
          </xsl:for-each>
          <xsl:for-each select="Editors/*[name() != 'header']">
            <xsl:sort select="position()" data-type="number" order="descending"/>
            <xsl:call-template name="editor"/>
          </xsl:for-each>
          <!--MAIN EDITOR-->
          <xsl:call-template name="main"/>
          <!--FAQ-->
          <xsl:for-each select="Other/*">
            <xsl:sort select="position()" data-type="number" order="descending"/>
            <xsl:call-template name="faq"/>
          </xsl:for-each>
          <xsl:for-each select="Editors/*">
            <xsl:sort select="position()" data-type="number" order="descending"/>
            <xsl:call-template name="faq"/>
          </xsl:for-each>
          <xsl:call-template name="faq"/>
          <!--GLOSSARY DEFINITIONS-->
          <div class="glossary">
            <h1 class="glossary">
              <img src="{@icon}" title="Glossary of rom hacking terms"/>
              Glossary
            </h1>
            <div class="glossary_box">
              <xsl:for-each select="Glossary/entry">
                <xsl:call-template name="definition"/>
              </xsl:for-each>
            </div>
          </div>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>

  <xsl:template name="lefttabs">
    <div class="lefttab">
      <p class="lefttab">
        <!--this link will bring the editor's help and FAQ windows to the front-->
        <a href="javascript:void(0)" onclick="ShowHide('{@form}', '{concat('faq_', @form)}')"
           title="{concat('Show ', @title, ' help window and FAQ')}">
          <img src="{@icon}" />
          <xsl:value-of select="@title" />
        </a>
      </p>
    </div>
  </xsl:template>
  
  <xsl:template name="main">
    <div class="editor" id="{@form}">
      <h1 class="faq">
        <img src="{@icon}" title="{@title}"/>
        <xsl:value-of select="@title"/>
      </h1>
      <div class="editor_box">
        <!--WRITE PROPERTIES-->
        <xsl:for-each select="Properties/@*">
          <div class="property">
            <h1 class="property">
              <xsl:value-of select="name()"/>
            </h1>
            <xsl:value-of select="."/>
          </div>
        </xsl:for-each>
        <!--WRITE DISCLAIMER-->
        <h1 class="subwindow">
          <xsl:value-of select="Read/header"/>
        </h1>
        <div class="subwindow">
          <p class="subwindow">
            <xsl:call-template name="breaklines">
              <xsl:with-param name="text" select="Read/introduction"/>
            </xsl:call-template>
          </p>
          <xsl:for-each select="Read/precaution">
            <div class="section">
              <p class="section">
                <xsl:value-of select="text()"/>
              </p>
            </div>
          </xsl:for-each>
          <p class="subwindow">
            <xsl:call-template name="breaklines">
              <xsl:with-param name="text" select="Read/conclusion"/>
            </xsl:call-template>
          </p>
        </div>
        <!--WRITE SECTIONS-->
        <xsl:for-each select="section">
          <h1 class="subwindow">
            <xsl:value-of select="header"/>
          </h1>
          <div class="subwindow">
            <p class="subwindow">
              <xsl:call-template name="breaklines">
                <xsl:with-param name="text" select="body"/>
              </xsl:call-template>
            </p>
          </div>
        </xsl:for-each>
        <h1 class="subwindow">
          <xsl:value-of select="Files/header"/>
        </h1>
        <div class="subwindow">
          <p class="subwindow">
            <xsl:call-template name="breaklines">
              <xsl:with-param name="text" select="Files/body"/>
            </xsl:call-template>
          </p>
          <div class="tooltip_box">
            <xsl:for-each select="Files/file">
              <div class="tooltip">
                <h1 class="tooltip">
                  <xsl:value-of select="@name"/>
                </h1>
                <p class="tooltip">
                  <xsl:value-of select="description"/>
                </p>
              </div>
            </xsl:for-each>
          </div>
        </div>
      </div>
    </div>
  </xsl:template>

  <xsl:template name="editor">
    <div class="editor" id="{@form}">
      <h1 class="editor">
        <img src="{@icon}" title="{@title}"/>
        <xsl:value-of select="@title"/>
        <!--<a href="{concat('screens/', @form, '_100%.png')}" target="new">
          <img src="{'icons/openTileEditor.gif'}" title="{'View Editor Screenshot'}" style="{'float: right'}"/>
        </a>-->
      </h1>
      <div class="editor_box">
        <!--EDITOR'S DESCRIPTION-->
        <p class="editor">
          <xsl:call-template name="formattext">
            <xsl:with-param name="text" select="description" />
          </xsl:call-template>
        </p>
        <!--EDITOR'S ATTRIBUTES-->
        <xsl:for-each select="./*[name() = 'attribute']">
          <xsl:call-template name="attribute"/>
        </xsl:for-each>
        <!--EDITOR'S TOOLTIPS-->
        <xsl:call-template name="tooltips" />
        <!--EDITOR'S SECTIONS & SUBWINDOWS-->
        <xsl:for-each select="./*">
          <xsl:if test="@type = 'section'">
            <xsl:call-template name="section"/>
          </xsl:if>
          <xsl:if test="@type = 'subwindow'">
            <xsl:call-template name="subwindow"/>
          </xsl:if>
        </xsl:for-each>
      </div>
    </div>
  </xsl:template>
  
  <xsl:template name="attribute">
    <p class="attribute">
      <xsl:call-template name="formattext">
        <xsl:with-param name="text" select="description"/>
      </xsl:call-template>
    </p>
  </xsl:template>
  <xsl:template name="subwindow">
    <h1 class="subwindow">
      <xsl:if test="@icon != ''">
        <img src="{@icon}" title="{@title}"/>
      </xsl:if>
      <xsl:value-of select="@title" />
    </h1>
    <div class="subwindow">
      <!--SUBWINDOW'S DESCRIPTION-->
      <xsl:if test="description != ''">
        <p class="subwindow">
          <xsl:call-template name="formattext">
            <xsl:with-param name="text" select="description" />
          </xsl:call-template>
        </p>
      </xsl:if>
      <!--SUBWINDOW'S ATTRIBUTES-->
      <xsl:for-each select="./*">
        <xsl:if test="name() = 'attribute'">
          <xsl:call-template name="attribute"/>
        </xsl:if>
      </xsl:for-each>
      <!--SUBWINDOW'S TOOLTIPS-->
      <xsl:call-template name="tooltips" />
      <!--SUBWINDOW'S SECTIONS-->
      <xsl:for-each select="./*">
        <xsl:if test="@type = 'section'">
          <xsl:call-template name="section" />
        </xsl:if>
      </xsl:for-each>
    </div>
  </xsl:template>
  
  <xsl:template name="subeditor">
    <h1 class="subeditor">
      <xsl:if test="@icon != ''">
        <img src="{@icon}" />
      </xsl:if>
      <xsl:value-of select="@title" />
    </h1>
    <div class="subeditor">
      <!--SUBWINDOW'S DESCRIPTION-->
      <xsl:if test="description != ''">
        <p class="subeditor">
          <xsl:call-template name="formattext">
            <xsl:with-param name="text" select="description" />
          </xsl:call-template>
        </p>
      </xsl:if>
      <!--SUBWINDOW'S ATTRIBUTES-->
      <xsl:for-each select="./*">
        <xsl:if test="name() = 'attribute'">
          <xsl:call-template name="attribute"/>
        </xsl:if>
      </xsl:for-each>
      <!--SUBWINDOW'S TOOLTIPS-->
      <xsl:call-template name="tooltips" />
      <!--SUBWINDOW'S SECTIONS-->
      <xsl:for-each select="./*">
        <xsl:if test="@type = 'section'">
          <xsl:call-template name="section" />
        </xsl:if>
      </xsl:for-each>
    </div>
  </xsl:template>
  
  <xsl:template name="section">
    <div class="section">
      <h1 class="section">
        <xsl:if test="@icon != ''">
          <img src="{@icon}" title="{@title}"/>
        </xsl:if>
        <xsl:value-of select="@title"/>
      </h1>
      <p class="section">
        <!--SECTION'S DESCRIPTION-->
        <xsl:if test="description != ''">
          <p class="section">
            <xsl:call-template name="formattext">
              <xsl:with-param name="text" select="description" />
            </xsl:call-template>
          </p>
        </xsl:if>
        <!--SECTION'S TOOLTIPS-->
        <xsl:call-template name="tooltips" />
        <!--SECTION'S SUBEDITORS-->
        <xsl:for-each select="./*">
          <xsl:if test="@type = 'subeditor'">
            <xsl:call-template name="subeditor"/>
          </xsl:if>
        </xsl:for-each>
      </p>
    </div>
  </xsl:template>
  
  <xsl:template name="tooltips">
    <xsl:if test="count(tooltip) > 0">
      <div class="tooltips">
        <h1 class="tooltips">
          Tooltips
        </h1>
        <div class="tooltip_box">
          <xsl:for-each select="./*">
            <xsl:if test="name() = 'tooltip'">
              <div class="tooltip">
                <h1 class="tooltip">
                  <xsl:value-of select="title"/>
                </h1>
                <p class="tooltip">
                  <xsl:call-template name="formattext">
                    <xsl:with-param name="text" select="description" />
                  </xsl:call-template>
                </p>
              </div>
            </xsl:if>
          </xsl:for-each>
        </div>
      </div>
    </xsl:if>
  </xsl:template>
  
  <!--FAQ-->
  <xsl:template name="faq">
    <div class="faq" id="{concat('faq_', @form)}">
      <h1 class="faq">
        <img src="{//LAZYSHELL/@icon}" title="Frequently Asked Questions"/>
        FAQ
      </h1>
      <div class="faq_box">
        <xsl:for-each select ="FAQ/entry">
          <h1 class="entry">
            <xsl:for-each select="question">
              <xsl:value-of select="text()"/>
              <br/>
            </xsl:for-each>
          </h1>
          <div class="entry">
            <p class="entry">
              <xsl:call-template name="formattext">
                <xsl:with-param name="text" select="answer" />
              </xsl:call-template>
            </p>
          </div>
        </xsl:for-each>
      </div>
    </div>
  </xsl:template>
  
  <!--definition-->
  <xsl:template name="definition">
    <a name="{@term}"></a>
    <h1 class="entry">
      <xsl:value-of select="@term"/>
    </h1>
    <div class="entry">
      <p class="entry">
        <xsl:call-template name="breaklines">
          <xsl:with-param name="text" select="definition" />
        </xsl:call-template>
      </p>
    </div>
  </xsl:template>
  
  <!--FORMAT TEXT-->
  <xsl:template name="formattext">
    <xsl:param name="text"/>
    <xsl:call-template name="glossarize">
      <xsl:with-param name="text" select="$text"/>
      <xsl:with-param name="entries" select="//LAZYSHELL/Glossary/entry"/>
    </xsl:call-template>
  </xsl:template>
  
  <!--HYPERLINK GLOSSARY REFERENCES-->
  <xsl:template name="glossarize">
    <xsl:param name="text" />
    <xsl:param name="entries" />
    <xsl:choose>
      <xsl:when test="not($entries)">
        <xsl:call-template name="breaklines">
          <xsl:with-param name="text" select="$text" />
        </xsl:call-template>
      </xsl:when>
      <xsl:when test="not(string($text))" />
      <xsl:otherwise>
        <xsl:variable name="term" select="$entries[1]/@term" />
        <xsl:variable name="punc" select="concat('.,;:( )[  ]!?$@&amp;&quot;', &quot;&apos;&quot;)"/>
        <xsl:variable name="before" select="substring-before($text, $term)"/>
        <xsl:variable name="before-char" select="substring(concat(' ', $before), string-length($before) + 1, 1)"/>
        <xsl:variable name="after" select="substring-after($text, $term)"/>
        <xsl:variable name="after-char" select="substring($after, 1, 1)"/>
        <xsl:choose>
          <xsl:when test="contains($text, $term) and
                          (not(normalize-space($before-char)) or contains($punc, $before-char)) and 
                          (not(normalize-space($after-char)) or contains($punc, $after-char))">
            <xsl:call-template name="glossarize">
              <xsl:with-param name="text" select="$before" />
              <xsl:with-param name="entries" select="$entries[position() >  1]" />
            </xsl:call-template>
            <!--underline w/dotted line, set definition as tooltip-->
            <span class="term" title="{$entries[1]/definition}">
              <xsl:value-of select="$term" />
            </span>
            <xsl:call-template name="glossarize">
              <xsl:with-param name="text" select="$after" />
              <xsl:with-param name="entries" select="$entries" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            <xsl:call-template name="glossarize">
              <xsl:with-param name="text" select="$text" />
              <xsl:with-param name="entries" select="$entries[position() >  1]" />
            </xsl:call-template>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <!--BREAK LINES-->
  <xsl:template name="breaklines">
    <xsl:param name="text"/>
    <xsl:choose>
      <xsl:when test="contains($text,'&#xA;')">
        <xsl:value-of select="substring-before($text,'&#xA;')"/>
        <br/>
        <xsl:call-template name="breaklines">
          <xsl:with-param name="text">
            <xsl:value-of select="substring-after($text,'&#xA;')"/>
          </xsl:with-param>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="replace">
    <xsl:param name="text" />
    <xsl:param name="replace" />
    <xsl:param name="by" />
    <xsl:choose>
      <xsl:when test="contains($text, $replace)">
        <xsl:value-of select="substring-before($text, $replace)" />
        <xsl:value-of select="$by" />
        <xsl:call-template name="replace">
          <xsl:with-param name="text" select="substring-after($text, $replace)" />
          <xsl:with-param name="replace" select="$replace" />
          <xsl:with-param name="by" select="$by" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
